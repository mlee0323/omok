using System;
using System.Collections.Generic;
using System.Linq;
using Omok_Server2.Network;

namespace Omok_Server2.Data
{
    public class Room
    {
        // ───────────────────────────────
        // 기본 속성
        // ───────────────────────────────
        public string RoomCode { get; }
        public int MaxPlayers { get; }
        public IReadOnlyList<ClientHandler> Clients => _clients.AsReadOnly();
        public IReadOnlyDictionary<ClientHandler, int> Teams => _teams;

        private List<ClientHandler> _clients = new();
        private Dictionary<ClientHandler, int> _teams = new(); // 1: A팀, 2: B팀
        private Dictionary<ClientHandler, bool> readyStatus = new();
        private HashSet<ClientHandler> loadingClients = new();

        public bool IsFull => _clients.Count >= MaxPlayers;
        public bool IsEmpty => _clients.Count == 0;

        private string blackTeam = "A";
        private string whiteTeam = "B";

        private int stoneCount = 0;

        // ───────────────────────────────
        // 게임 관련 상태
        // ───────────────────────────────
        private bool gameStarted = false;
        // private int turnIndex = 0;
        private string currentTurnTeam = "A";
        private List<ClientHandler> teamAOrder = new();
        private List<ClientHandler> teamBOrder = new();
        private int[,] board = new int[19, 19]; // 0: 없음, 1: A, 2: B

        // 팀별로 인덱스 따로 관리
        private int indexA = -1;
        private int indexB = -1;

        // 턴 타이머
        private System.Timers.Timer? turnTimer;
        private const int TurnTimeLimitMs = 30000;

        // ───────────────────────────────
        // 생성자
        // ───────────────────────────────
        public Room(string code, ClientHandler host, int maxPlayers)
        {
            RoomCode = code;
            MaxPlayers = maxPlayers;
        }

        // ───────────────────────────────
        // 참가 / 퇴장 / 팀 관리
        // ───────────────────────────────
        public bool AddClient(ClientHandler client)
        {
            if (IsFull) return false;
            _clients.Add(client);
            _teams[client] = (_clients.Count % 2 == 0) ? 2 : 1;
            readyStatus[client] = false;
            return true;
        }

        public bool RemoveClient(ClientHandler client)
        {
            if (!_clients.Contains(client)) return false;
            _clients.Remove(client);
            _teams.Remove(client);
            return true;
        }

        public bool ChangeTeam(ClientHandler client)
        {
            if (!_teams.ContainsKey(client) || IsAnyLoading()) return false;
            int cur = _teams[client];
            _teams[client] = cur == 1 ? 2 : 1;
            BroadcastNotMe("TEAM_CHANGE", client);
            return true;
        }

        public int? GetTeam(ClientHandler client) =>
            _teams.TryGetValue(client, out int team) ? team : null;

        // ───────────────────────────────
        // Ready / Loading 상태 관리
        // ───────────────────────────────
        public bool SetReady(ClientHandler client, bool ready)
        {
            if (!_clients.Contains(client)) return false;
            readyStatus[client] = ready;
            CheckGameStartCondition();
            return true;
        }

        public bool IsReady(ClientHandler client)
        {
            return readyStatus.TryGetValue(client, out bool isReady) && isReady;
        }

        private bool IsEveryoneReady() =>
            _clients.All(c => readyStatus.TryGetValue(c, out var rdy) && rdy);

        private bool HasTwoTeams() =>
            GetTeamMembers(1).Count > 0 && GetTeamMembers(2).Count > 0;

        public void SetLoading(ClientHandler client, bool loading)
        {
            if (loading) loadingClients.Add(client);
            else loadingClients.Remove(client);
        }

        public bool IsAnyLoading() => loadingClients.Count > 0;
        public bool IsClientLoading(ClientHandler client) => loadingClients.Contains(client);

        // ───────────────────────────────
        // 게임 시작 조건 확인 및 실행
        // ───────────────────────────────
        public void CheckGameStartCondition()
        {
            if (gameStarted) return;
            if (IsEveryoneReady() && HasTwoTeams())
                StartGame();
        }

        public void StartGame()
        {
            gameStarted = true;

            teamAOrder = GetTeamMembers(1).OrderBy(_ => Guid.NewGuid()).ToList();
            teamBOrder = GetTeamMembers(2).OrderBy(_ => Guid.NewGuid()).ToList();

            currentTurnTeam = new Random().Next(2) == 0 ? "A" : "B";


            blackTeam = currentTurnTeam;  // 선공팀은 흑돌로 고정
            whiteTeam = (blackTeam == "A") ? "B" : "A";

            indexA = -1;
            indexB = -1;

            // 선공팀에게 바로 첫 턴을 부여
            if (currentTurnTeam == "A")
            {
                indexA = 0; // A팀 리스트의 0번 플레이어가 첫 턴
                var firstA = teamAOrder[indexA];
                Broadcast($"GAME_START|A");
                Broadcast($"TURN_INFO|A|0|{firstA.getNickname()}|{stoneCount}");
            }
            else /* currentTurnTeam == "B" */
            {
                indexB = 0; // B팀 리스트의 0번 플레이어가 첫 턴
                var firstB = teamBOrder[indexB];
                Broadcast($"GAME_START|B");
                Broadcast($"TURN_INFO|B|0|{firstB.getNickname()}|{stoneCount}");
            }

            StartTurnTimer();

            //Broadcast($"GAME_START|{blackTeam}"); // 흑돌 팀을 알림
            //AdvanceTurn(); // 바로 턴 시작
        }


        public void ResetGame()
        {
            gameStarted = false;
            StopTurnTimer();
            //turnIndex = 0;
            currentTurnTeam = "A";
            board = new int[19, 19];
            teamAOrder.Clear();
            teamBOrder.Clear();
            foreach (var key in readyStatus.Keys.ToList())
                readyStatus[key] = false;
        }

        // ───────────────────────────────
        // 턴 관련 처리
        // ───────────────────────────────
        //public ClientHandler? GetCurrentTurnPlayer()
        //{
        //    var list = currentTurnTeam == "A" ? teamAOrder : teamBOrder;
        //    return list.Count > 0 ? list[turnIndex % list.Count] : null;
        //}
        public ClientHandler? GetCurrentTurnPlayerByTeam()
        {
            if (currentTurnTeam == "A")
            {
                if (teamAOrder.Count == 0) return null;
                return teamAOrder[indexA];
            }
            else
            {
                if (teamBOrder.Count == 0) return null;
                return teamBOrder[indexB];
            }
        }

        public void AdvanceTurn()
        {
            StopTurnTimer();
            //turnIndex++;
            currentTurnTeam = (currentTurnTeam == "A") ? "B" : "A";

            ClientHandler? currentPlayer = null;
            if (currentTurnTeam == "A")
            {
                // A 팀의 경우
                if (teamAOrder.Count == 0) return;
                indexA = (indexA + 1) % teamAOrder.Count;
                currentPlayer = teamAOrder[indexA];
            }
            else 
            {
                if (teamBOrder.Count == 0) return;
                indexB = (indexB + 1) % teamBOrder.Count;
                currentPlayer = teamBOrder[indexB];
            }


            //var currentPlayer = GetCurrentTurnPlayer();
            var nickname = currentPlayer?.getNickname() ?? "?";
            //Broadcast($"TURN_INFO|{currentTurnTeam}|{turnIndex}|{nickname}|{stoneCount}");
            Broadcast($"TURN_INFO|{currentTurnTeam}|{/*turnNumber*/stoneCount}|{nickname}|{stoneCount}");


            StartTurnTimer();
        }

        private void StartTurnTimer()
        {
            turnTimer?.Stop();
            turnTimer = new System.Timers.Timer(TurnTimeLimitMs);
            turnTimer.Elapsed += (s, e) =>
            {
                turnTimer?.Stop();
                AdvanceTurn();
            };
            turnTimer.AutoReset = false;
            turnTimer.Start();
        }

        private void StopTurnTimer()
        {
            turnTimer?.Stop();
            turnTimer = null;
        }

        // ───────────────────────────────
        // 오목 게임 진행 (돌 놓기 + 승리 판정)
        // ───────────────────────────────
        public bool PlaceStone(int x, int y, int team)
        {
            if (x < 0 || x >= 19 || y < 0 || y >= 19) return false;
            if (board[x, y] != 0) return false;

            board[x, y] = team;

            stoneCount++;

            return CheckWin(x, y, team);
        }

        private bool CheckWin(int x, int y, int team)
        {
            int[][] directions = new int[][]
            {
                new[] {1, 0},  new[] {0, 1},
                new[] {1, 1},  new[] {1, -1},
            };

            foreach (var dir in directions)
            {
                int count = 1;
                for (int d = 1; d <= 4; d++)
                {
                    int nx = x + dir[0] * d;
                    int ny = y + dir[1] * d;
                    if (nx < 0 || ny < 0 || nx >= 19 || ny >= 19) break;
                    if (board[nx, ny] == team) count++; else break;
                }
                for (int d = 1; d <= 4; d++)
                {
                    int nx = x - dir[0] * d;
                    int ny = y - dir[1] * d;
                    if (nx < 0 || ny < 0 || nx >= 19 || ny >= 19) break;
                    if (board[nx, ny] == team) count++; else break;
                }
                if (count >= 5) return true;
            }
            return false;
        }

        private List<ClientHandler> GetTeamMembers(int teamNumber) =>
            _clients.Where(c => _teams[c] == teamNumber).ToList();

        // ───────────────────────────────
        // 메시지 브로드캐스트
        // ───────────────────────────────
        public void Broadcast(string msg)
        {
            foreach (var c in _clients) c.Send(msg);
        }

        public void BroadcastNotMe(string msg, ClientHandler client)
        {
            foreach (var c in _clients)
                if (c != client) c.Send(msg);
        }
    }
}
