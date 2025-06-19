using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Omok_Server2.Models;

namespace Omok_Server2.Data
{
    public class Room
    {
        // ───────────────────────────────
        // 기본 속성
        // ───────────────────────────────
        public string RoomCode { get; }
        public int MaxPlayers { get; }

        private int gameResult = 0;     // -1 -> 무승부, 0 -> 승패 결정 아직 안남, team 값 -> 승리팀

        public IReadOnlyList<ClientHandler> Clients => _clients.AsReadOnly();
        public IReadOnlyDictionary<ClientHandler, int> Teams => _teams;

        private List<ClientHandler> _clients = new();
        private Dictionary<ClientHandler, int> _teams = new(); // 1: A팀, 2: B팀

        private Dictionary<ClientHandler, bool> readyStatus = new();
        private HashSet<ClientHandler> loadingClients = new();

        private Dictionary<ClientHandler, Player> _players = new(); // 플레이어의 정보를 담음

        public bool IsFull => _clients.Count >= MaxPlayers;
        public bool IsEmpty => _clients.Count == 0;

        private string blackTeam = "A";
        private string whiteTeam = "B";

        private char currentStoneColor = 'B';

        private int stoneCount = 0;

        private int room_pk = 0;
        public DateTime StartTime { get; private set; }     // 게임 시작 시간
        public DateTime EndTime { get; private set; }       // 게임 종료 시간

        // ───────────────────────────────
        // 게임 관련 상태
        // ───────────────────────────────
        private bool gameStarted = false;
        // private int turnIndex = 0;
        private string currentTurnTeam = "A";
        private List<ClientHandler> teamAOrder = new();
        private List<ClientHandler> teamBOrder = new();
        private int boardSize;
        private int[,] board; // 0: 없음, 1: A, 2: B

        // 팀별로 인덱스 따로 관리
        private int indexA = -1;
        private int indexB = -1;

        // 턴 타이머
        private System.Timers.Timer? turnTimer;
        private const int TurnTimeLimitMs = 30000;

        private Random random = new Random();

        // ───────────────────────────────
        // 생성자
        // ───────────────────────────────
        public Room(string code, ClientHandler host, int maxPlayers)
        {
            RoomCode = code;
            MaxPlayers = maxPlayers;

            boardSize = 19;
            board = new int[boardSize, boardSize];
        }

        /// ───────────────────────────────
        /// 초기화
        /// ───────────────────────────────
        public void ResetGame()
        {
            // 1. 방 상태 변경
            gameStarted = false;
            gameResult = 0;

            // 2. 타이머 멈추기
            StopTurnTimer();
            
            // 3. 변수 초기화
            //turnIndex = 0;
            currentTurnTeam = "A";
            teamAOrder.Clear();
            teamBOrder.Clear();

            // 4. 바둑판 초기화
            board = new int[boardSize, boardSize];
            stoneCount = 0;

            // 5. 플레이어들의 준비 상태 풀기
            foreach (ClientHandler c in _clients)
            {
                readyStatus[c] = false;

                // _players
                _players[c].SetReady(false);
            }
        }


        // ───────────────────────────────
        // 참가 / 퇴장 / 팀 관리
        // ───────────────────────────────
        public bool AddClient(ClientHandler client)
        {
            if (IsFull || gameStarted) return false;

            _clients.Add(client);
            
            // 비어있는 팀에 먼저 들어가도록 팀 할당
            int teamACount = _teams.Values.Count(t => t == 1);
            int teamBCount = _teams.Values.Count(t => t == 2);
            
            int assignedTeam;
            if (teamACount <= teamBCount && teamACount < 2) // A팀이 더 적거나 같고, A팀이 2명 미만이면 A팀
            {
                assignedTeam = 1;
            }
            else if (teamBCount < 2) // B팀이 2명 미만이면 B팀
            {
                assignedTeam = 2;
            }
            else // 둘 다 가득 찬 경우 A팀에 할당 (이론적으로는 발생하지 않아야 함)
            {
                assignedTeam = 1;
            }
            
            _teams[client] = assignedTeam;
            readyStatus[client] = false;

            // _players
            _players[client] = new Player(
                    int.Parse(client.getUserPk()),
                    client.getNickname(),
                    assignedTeam
            );

            return true;
        }

        public bool RemoveClient(ClientHandler client)
        {
            if (!_clients.Contains(client)) return false;
            _clients.Remove(client);
            _teams.Remove(client);

            //_players
            _players.Remove(client);

            return true;
        }

        public bool ChangeTeam(ClientHandler client)
        {
            // 클라이언트가 팀에 없거나, 누군가 들어오려고 하고 있거나, 이미 준비된 유저는 팀 변경 불가
            if (!_teams.ContainsKey(client) || IsAnyLoading() || readyStatus[client])
                return false;

            int cur = _teams[client];
            _teams[client] = cur == 1 ? 2 : 1;
            BroadcastNotMe("TEAM_CHANGE", client);

            // _players
            if (!_players.ContainsKey(client)) return false;
            
            _players[client].SetTeam(_teams[client]);

            return true;
        }

        public void ShuffleTeams()
        {
            if (_clients.Count == 0) return;

            // 준비된 유저들은 고정, 준비 안된 유저만 셔플 대상
            var readyClients = _clients.Where(c => readyStatus[c]).ToList();
            var nonReadyClients = _clients.Except(readyClients).ToList();
            if (nonReadyClients.Count == 0) return; // 모두 준비된 상태면 아무 것도 안 함

            int attempts = 0;            
            do
            {
                foreach (var c in nonReadyClients)
                {
                    _teams[c] = random.Next(1, 3);
                    _players[c].SetTeam(_teams[c]);
                }
                attempts++;
            } while (!HasTwoTeams() && attempts< 5);

            // 준비 상태 유지
            //// 준비 상태 초기화
            //foreach (var c in _clients)
            //{
            //    readyStatus[c] = false;
            //    _players[c].SetReady(false);
            //}
        }

        public int? GetTeam(ClientHandler client) =>
            _teams.TryGetValue(client, out int team) ? team : null;
        // _player[client].GetTeam();

        // ───────────────────────────────
        // Ready / Loading 상태 관리
        // ───────────────────────────────
        public bool SetReady(ClientHandler client, bool ready)
        {
            if (!_clients.Contains(client)) return false;
            readyStatus[client] = ready;
            CheckGameStartCondition();

            // _players
            if (!_clients.Contains(client)) return false;
            _players[client].SetReady(ready);

            return true;
        }

        public bool IsReady(ClientHandler client)
        {
            return readyStatus.TryGetValue(client, out bool isReady) && isReady;
        }

        // TODO: _players에도 ready 처리 필요할 가능성 있음
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
            StartTime = DateTime.Now;       // 시작 시간은 현재 시간으로 설정

            teamAOrder = GetTeamMembers(1).OrderBy(_ => Guid.NewGuid()).ToList();
            teamBOrder = GetTeamMembers(2).OrderBy(_ => Guid.NewGuid()).ToList();

            currentStoneColor = 'B';                // 항상 시작은 흑돌
            int teamForBlack = random.Next(1, 3);
            blackTeam = (teamForBlack == 1) ? "A" : "B";
            whiteTeam = (blackTeam == "A") ? "B" : "A";
            currentTurnTeam = blackTeam;  // 흑돌 팀이 항상 선공으로

            if (currentTurnTeam == "A")
            {
                indexA = random.Next(0, teamAOrder.Count);
                var firstA = teamAOrder[indexA];
                Broadcast($"GAME_START|A");
                Broadcast($"TURN_INFO|A|0|{firstA.getNickname()}|{stoneCount}");
            }
            else
            {
                indexB = random.Next(0, teamBOrder.Count);
                var firstB = teamBOrder[indexB];
                Broadcast($"GAME_START|B");
                Broadcast($"TURN_INFO|B|0|{firstB.getNickname()}|{stoneCount}");
            }

            StartTurnTimer();

            // DB 등록
            room_pk = GameModel.SaveRoom(0, RoomCode, StartTime, EndTime);
            foreach (var c in _clients)
            {
                if (_players[c].GetTeam() == teamForBlack)
                    _players[c].SetStoneColor('B');
                else
                    _players[c].SetStoneColor('W');

                _players[c].SetTeamPk(GameModel.SaveTeam(room_pk, _players[c].GetPK(), _players[c].GetTeam(), _players[c].GetStoneColor()));
            }

            foreach (var c in _clients)
            {
                bool isBlack = (_players[c].GetTeam() == teamForBlack);
                _players[c].SetStoneColor(isBlack ? 'B' : 'W');
                _players[c].SetTeamPk(
                        GameModel.SaveTeam(
                                room_pk,
                                _players[c].GetPK(),
                                _players[c].GetTeam(),
                                _players[c].GetStoneColor()
                        ));
            }

            //Broadcast($"GAME_START|{blackTeam}"); // 흑돌 팀을 알림
            //AdvanceTurn(); // 바로 턴 시작
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
        public bool PlaceStone(int x, int y, ClientHandler client)
        {
            if (x < 0 || x >= boardSize || y < 0 || y >= boardSize) return false;
            if (board[x, y] != 0) return false;

            board[x, y] = (int)GetTeam(client);

            stoneCount++;

            if (_players[client] != null)
                GameModel.RecordStone(room_pk, _players[client].GetPK(), _players[client].GetName(), _players[client].GetTeam(), x, y, _players[client].GetStoneColor());

            // return CheckWin(x, y, team);
            return CheckWin(x, y, board[x, y]);
        }

        public bool DeleteStone(int x,int y, int team, ClientHandler client)
        {
            if (x < 0 || x >= boardSize || y < 0 || y >= boardSize) return false;
            board[x, y] = 0;

            stoneCount--;

            // 삭제한 돌 기록
            if (_players[client] != null)
                GameModel.RecordStone(room_pk, _players[client].GetPK(), _players[client].GetName(), _players[client].GetTeam(), x, y, _players[client].GetStoneColor(), 1); // 지우기는 skill을 1로 설정
            return CheckWin(x, y, team);
        }

        // 엎기 처리
        public void TurnOver()
        {
            UpdateResult();
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
                    if (nx < 0 || ny < 0 || nx >= boardSize || ny >= boardSize) break;
                    if (board[nx, ny] == team) count++; else break;
                }
                for (int d = 1; d <= 4; d++)
                {
                    int nx = x - dir[0] * d;
                    int ny = y - dir[1] * d;
                    if (nx < 0 || ny < 0 || nx >= boardSize || ny >= boardSize) break;
                    if (board[nx, ny] == team) count++; else break;
                }
                if (count >= 5)
                {
                    // DB 등록
                    SetGameResult(team);
                    UpdateResult();
                    return true;
                }

                // 더 이상 둘 곳이 없으면 무승부 처리
                if (stoneCount == boardSize * boardSize)
                {
                    SetGameResult(-1);
                    UpdateResult();
                    return true;
                }
            }
            return false;
        }

        private void UpdateResult()
        {
            GameModel.UpdateRoom(room_pk, RoomCode, StartTime, DateTime.Now);
            foreach (var c in _clients)
                if (c != null)
                {
                    int team = _players[c].GetTeam();
                    
                    char win;
                    if (gameResult == team)
                        win = 'W'; 
                    else if (gameResult == -1) // 팀이 -1이면 무승부 처리
                        win = 'D';
                    else
                        win = 'L';

                    GameModel.UpdateTeam(
                            _players[c].GetTeamPK(),
                            room_pk,
                            _players[c].GetPK(),
                            _players[c].GetTeam(),
                            _players[c].GetStoneColor(),
                            win
                    );
                }
        }

        private List<ClientHandler> GetTeamMembers(int teamNumber) =>
            _clients.Where(c => _teams[c] == teamNumber).ToList();

        //private List<ClientHandler> GetTeamMembers(int teamNumber)
        //{
        //    List<Player> result = new List<Player>();
        //    foreach (var c in _clients)
        //    {
        //        if (_players[c].GetTeam() == teamNumber)
        //            result.Add(_players[c]);
        //    }
        //}

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

        /// ───────────────────────────────
        /// Getter & Setter
        /// ───────────────────────────────
        public int GetGameResult()
        {
            return gameResult;
        }

        public void SetGameResult(int gameResult)
        {
            this.gameResult = gameResult;
        }
    }
}
