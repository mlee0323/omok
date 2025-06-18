using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;
using Omok_Client.Control;
using Omok_Client.Network;
using Omok_Client.Util;

namespace Omok_Client.Form
{
    public partial class InGame : MetroForm
    {
        private string roomCode;
        private bool isInitialized = false;
        private bool isRunning = false;
        private bool isReady = false; // 게임 준비 여부

        private bool gameStarted = false;
        private string blackTeam = "A";       // "A" 또는 "B"
        private string currentTurnTeam = "";  // 현재 턴 팀
        private int currentTurnIndex = 0;            // 팀 내 순번

        private int timeAddUse = 2;
        private int timeRemoveUse = 2;

        private bool deletingStoneEnabled = false;  // 돌 삭제 기능 활성화 여부
        private Point deletingStonePos = new Point(-1, -1);  // 삭제할 돌의 위치
        private int deleteStoneUses = 2;  // 돌 삭제 사용 횟수



        private GoBoardControl board; // 바둑판 컨트롤
        private ChatForm chatForm; // 채팅 폼

        private System.Windows.Forms.Timer turnTimer;
        private int remainingSeconds = 30;

        public InGame(string _roomCode)
        {
            InitializeComponent();
            roomCode = _roomCode;
            this.Text += roomCode;

            lbl_turn.Text = "";
            lbl_player.Text = "";

            // 초기에는 비활성화
            btn_move.Enabled = false;
            btn_ready.Enabled = false;
            pn_board.Enabled = false;

            // 바둑판 로드
            LoadBadukpan();

            // 채팅 폼 로드
            LoadChatForm();

            // 서버에서 팀 정보 받아오기
            LoadTeamInfoFromServer();
            MySelfLabel.Text = Session.Nickname;

            btn_skill_2.Text = $"시간 늘리기 ({timeAddUse})";
            btn_skill_2.Enabled = true;

            btn_skill_1.Text = $"지우기 ({timeRemoveUse})";
            btn_skill_1.Enabled = true;

        }
        private void InGame_Load(object sender, EventArgs e)
        {
            isRunning = true;
            Task.Run(() => ListenLoop());
        }

        private void LoadTeamInfoFromServer()
        {
            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되지 않았습니다.");
                this.Close();
                return;
            }

            try
            {
                // 요청 보내고 TEAM_INFO_END까지 수신
                var messages = Client.RequestResponse<string>(
                    $"TEAM_INFO|{Session.Pk}|{Session.Nickname}|{roomCode}",
                    "TEAM_INFO_END",
                    msg => msg);

                foreach (var msg in messages)
                {
                    if (msg.StartsWith("TEAM_INFO_FAIL"))
                    {
                        MessageBox.Show("팀 정보 로드 실패: " + msg);
                        this.Close();
                        return;
                    }
                    HandleTeamChanged(msg);
                }

                btn_move.Enabled = true;
                btn_ready.Enabled = true;
                pn_board.Enabled = true;
                isInitialized = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show("네트워크 오류: " + ex.Message);
                this.Close();
            }
        }

        private void HandleTeamChanged(string msg)
        {
            var tokens = msg.Split('|');
            if(tokens.Length == 0) return;

            var command = tokens[0];
            switch(command)
            {
                case "TEAM_CHANGED":
                case "TEAM_INFO":
                    var pk = tokens[1];
                    var nickname = tokens[2];
                    if (!int.TryParse(tokens[3], out int team)) return;
                    bool readyFlag = tokens.Length >= 5 && bool.TryParse(tokens[4], out bool r) && r;

                    if (pk == Session.Pk.ToString())
                    {
                        Session.Team = team;
                        board.MyTeam = team == 1 ? "A" : "B";
                    }

                    Invoke(new Action(() =>
                    {
                        RemovePlayerFromTeams(nickname);

                        var item = new ListViewItem(nickname) { Name = nickname };
                        if (team == 1)
                            lv_teamA.Items.Add(item);
                        else
                            lv_teamB.Items.Add(item);
                    }));
                    break;
                case "PLAYER_EXIT":
                    if (tokens.Length >= 2)
                    {
                        var ExitPlayerName = tokens[1];
                        Invoke(new Action(() => RemovePlayerFromTeams(ExitPlayerName)));
                    }
                    break;
                default:
                    // 알 수 없는 명령어
                    break;
            }
        }

        private void HandlePlayerReady(string msg)
        {
            var tokens = msg.Split('|');
            if (tokens.Length < 4) return;

            var playerPk = tokens[1];
            var ReadyPlayerName = tokens[2];
            if (!int.TryParse(tokens[3], out int teamNum)) return;
            var isReady = tokens.Length >= 5 && bool.TryParse(tokens[4], out bool ready) && ready;

            // Reflect change in UI
            Invoke(new Action(() => UpdatePlayerReadyState(ReadyPlayerName, isReady)));
        }

        private void RemovePlayerFromTeams(string playerName)
        {
            lv_teamA.Items.RemoveByKey(playerName);
            lv_teamB.Items.RemoveByKey(playerName);
        }

        private void UpdatePlayerReadyState(string playerName, bool isReady)
        {
            ListViewItem item = lv_teamA.Items.Find(playerName, false).FirstOrDefault() ?? lv_teamB.Items.Find(playerName, false).FirstOrDefault();
            if (item != null)
                item.Text = isReady ? $"{playerName} (Ready)" : playerName;
        }

        private void HandleTurnInfo(string msg)
        {
            string[] tokens = msg.Split('|');
            currentTurnTeam = tokens[1];
            currentTurnIndex = int.Parse(tokens[2]);
            string nickname = tokens.Length >= 4 ? tokens[3] : "(?)";
            string turnNumber = tokens.Length >= 5 ? tokens[4] : "?";

            board.SetCurrentTurn(currentTurnTeam);

            Invoke(new Action(() =>
            {
                lbl_player.Text = nickname;
                lbl_turn.Text = turnNumber;
                remainingSeconds = 30;
                lbl_timer.Text = $"{remainingSeconds}초";
                turnTimer?.Start();

                btn_skill_2.Enabled = (nickname == MySelfLabel.Text) && (timeAddUse > 0);
                btn_skill_1.Enabled = (nickname == MySelfLabel.Text) && (timeRemoveUse > 0);
            }));
        }

        private void HandleStonePut(string msg)
        {
            // msg: STONE_PUT|x|y|nickname|team
            string[] tokens = msg.Split('|');
            int x = int.Parse(tokens[1]);
            int y = int.Parse(tokens[2]);
            int team = int.Parse(tokens[4]);

            board.PlaceStone(x, y, team);
        }
        private void HandleStoneDelete(string msg)
        {
            

            string[] tokens = msg.Split('|');
            int x = int.Parse(tokens[1]);
            int y = int.Parse(tokens[2]);
            int team = int.Parse(tokens[4]);

            
            
            board.DeleteStone(x, y);
            deletingStone.Text = "";
        }

        private async void HandleGameOver(string msg)
        {
            string[] tokens = msg.Split('|');
            string winner = tokens[1];
            
            gameStarted = false;

            Invoke(new Action(() =>
            {
                if (winner == "0")
                {
                    AppendSystemMessage("게임이 종료되었습니다.");
                }
                else
                {
                    if (winner == "1")
                        winner = "A";
                    else if (winner == "2")
                        winner = "B";
                    else
                        winner = "Unknown";
                    AppendSystemMessage($"{winner} 팀이 승리했습니다!");
                }
                turnTimer?.Stop();
            }));

            board.EndGame();

            // 5초 대기 후 초기화
            await Task.Delay(5000);

            Invoke(new Action(() =>
            {
                lv_teamA.Items.Clear();
                lv_teamB.Items.Clear();
                lbl_timer.Text = "";
                lbl_player.Text = "";
                btn_move.Enabled = true;
                btn_exit.Enabled = true;
                btn_shuffle.Enabled = true;
                btn_ready.Enabled = true;
            }));
        }

        private void LoadBadukpan()
        {
            // 바둑판 컨트롤 로드
            board = new GoBoardControl();
            board.Dock = DockStyle.Fill;
            // board.OnTurnChanged += (msg) => showTurnLabel.Text = "현재 턴: " + msg;
            pn_board.Controls.Add(board);
        }

        private void LoadChatForm()
        {
            try
            {
                chatForm = new ChatForm();
                chatForm.Dock = DockStyle.Right;
                chatForm.Width = 310;
                chatForm.Visible = true;
                chatForm.BackColor = Color.White;
                this.Controls.Add(chatForm);
                chatForm.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"채팅 폼 로드 중 오류 발생: {ex.Message}");
            }
        }
        private void ListenLoop()
        {
            string msg = string.Empty;
            try
            {
                while (isRunning)
                {
                    msg = Client.Receive();
                    if (msg == null) continue;

                    if (msg.StartsWith("TEAM_CHANGED") || msg.StartsWith("PLAYER_EXIT"))
                        HandleTeamChanged(msg);
                    else if (msg.StartsWith("PLAYER_READY"))
                        HandlePlayerReady(msg);
                    else if (msg.StartsWith("GAME_START"))
                        HandleGameStart(msg);
                    else if (msg.StartsWith("TURN_INFO|"))
                        HandleTurnInfo(msg);
                    else if (msg.StartsWith("STONE_PUT|"))
                        HandleStonePut(msg);
                    else if (msg.StartsWith("GAME_OVER"))
                        HandleGameOver(msg);
                    else if (msg.StartsWith("CHAT"))
                        HandleChat(msg);
                    else if (msg.StartsWith("SKILL_USE|"))
                        HandleSkillUse(msg);
                    else if (msg.StartsWith("STONE_DEL|"))
                        HandleStoneDelete(msg);

                    // 기타 메시지 처리...
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 끊김: " + ex.Message + " > " + msg);
                this.Invoke(new Action(() => Close()));
            }
        }

        private void HandleGameStart(string msg)
        {
            // msg: "GAME_START|A"
            string[] tokens = msg.Split('|');
            blackTeam = tokens[1];
            currentTurnTeam = blackTeam;
            currentTurnIndex = 0;
            gameStarted = true;

            Invoke(new Action(() =>
            {
                btn_move.Enabled = false;
                btn_exit.Enabled = false;
                btn_shuffle.Enabled = false;
                btn_ready.Enabled = false;

                // 바둑판 다시 그리기 등 초기화가 필요하면 여기서
                AppendSystemMessage($"{blackTeam} 팀이 먼저 시작합니다!");
            }));

            board.StartGame(currentTurnTeam); // 바둑판에 시작 전달

            turnTimer = new System.Windows.Forms.Timer();
            turnTimer.Interval = 1000;
            turnTimer.Tick += (s, e) =>
            {
                remainingSeconds--;
                lbl_timer.Text = $"{remainingSeconds}초";
                if (remainingSeconds <= 0)
                {
                    turnTimer.Stop();
                    lbl_timer.Text = "시간 초과!";
                }
            };
        }

        private void HandleChat(string msg)
        {
            try
            {
                string[] tokens = msg.Split('|');
                if (tokens.Length >= 4)
                {
                    string nickname = tokens[2];
                    string message = tokens[3];
                    this.Invoke(new Action(() =>
                    {
                        if (chatForm != null)
                        {
                            chatForm.AppendChatMessage(nickname, message);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"채팅 메시지 처리 중 오류 발생: {ex.Message}");
            }
        }

        private void HandleSkillUse(string msg)
        {
            string[] tokens = msg.Split('|');
            string pk = tokens[1];
            string nickname = tokens[2];
            int skillType = int.Parse(tokens[3]);

            if (skillType == 1) // 돌 삭제 스킬
            {
                Invoke(new Action(() =>
                {
                    // 본인 차례일 때만 사용 가능
                    if (nickname != Session.Nickname)
                    {
                        return;
                    }

                    
                    deletingStoneEnabled = true;
                    

                    // 채팅창에 스킬 사용 메시지 표시
                    AppendSystemMessage($"{nickname}님이 돌 삭제 스킬을 사용했습니다.");
                }));
            }

                if (skillType == 3) // 바둑판 어지르기 스킬
            {
                Invoke(new Action(() =>
                {
                    // 바둑판 랜덤 배치
                    board.RandomizeBoard();
                    
                    // 채팅창에 메시지 표시
                    if (chatForm != null)
                    {
                        chatForm.AppendSystemMessage($"{nickname}님이 바둑판을 어지르고 갔습니다!");
                    }

                    // 게임 종료 처리
                    gameStarted = false;
                    turnTimer?.Stop();
                    board.EndGame();
                }));
            }



        }

        // 버튼 처리
        private void btn_move_Click(object sender, EventArgs e)
        {
            if (roomCode == null || !isInitialized) return;

            Client.Send($"CHANGE_TEAM|{Session.Pk}|{roomCode}");
        }

        private void btn_ready_Click(object sender, EventArgs e)
        {
            isReady = !isReady;
            btn_ready.Text = isReady ? "준비 해제" : "게임 준비";
            Client.Send($"GAME_READY|{Session.Pk}|{Session.Nickname}|{roomCode}|{isReady}");
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_skill_3_Click(object sender, EventArgs e)
        {
            if (!gameStarted) return;
            
            // 서버에 스킬 사용 메시지 전송
            Client.Send($"SKILL_USE|{Session.Pk}|{Session.Nickname}|{roomCode}|3");
        }

        private void AppendSystemMessage(string message)
        {
            if (chatForm != null)
            {
                chatForm.AppendSystemMessage(message);
            }
        }

        private void btn_skill_2_Click(object sender, EventArgs e)
        {
            if (timeAddUse <= 0)
                return;

            remainingSeconds += 15;
            timeAddUse--;

            btn_skill_2.Text = $"시간 늘리기 ({timeAddUse})";

            if (timeAddUse == 0)
                btn_skill_2.Enabled = false;
            AppendSystemMessage($"{Session.Nickname}님이 시간을 15초 연장했습니다.");
        }

        private void btn_skill_1_Click(object sender, EventArgs e)
        {
            if (deleteStoneUses <= 0)
                return;
            deletingStoneEnabled = true;
            board.SetDeletingStoneEnabled(true);

            deletingStone.Text = "돌을 지우고 착수하세요";
            deleteStoneUses--; // 사용 횟수 차감
            btn_skill_1.Text = $"지우기 ({deleteStoneUses})";

            // 사용 횟수 다 되면 버튼 비활성화
            if (deleteStoneUses == 0)
                btn_skill_1.Enabled = false;


            Client.Send($"SKILL_USE|{Session.Pk}|{Session.Nickname}|{roomCode}|1");
            
        }

       
        private void InGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ListenLoop 중단
            isRunning = false;

            // 서버에 방 나감 알림
            try
            {
                Client.Send($"EXIT_ROOM|{Session.Pk}|{Session.Nickname}|{roomCode}");
                Session.RoomCode = string.Empty;
            }
            catch { /* 네트워크 오류 무시 */ }
        }
    }
    }

