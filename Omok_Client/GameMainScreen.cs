using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;
using Pack_Server;

namespace Omok
{
    public partial class GameMainScreen : MetroForm
    {
        private ChatForm chatForm;

        private int turnNum = 1;

        private List<string> userNicknames = new List<string>(); // 로그인 시 서버에서 받은 유저 닉네임

        private NetworkStream _stream;
        private string _myNickName;
        private GoBoardControl board;
        private Thread networkThread;
        private string _myID;

        private bool hasSetInitialTurn = false;


        public GameMainScreen(List<string> nicknames, NetworkStream stream, string myNIckname,string myId)
        {
            InitializeComponent();

            _myID = myId;

            this.userNicknames = nicknames;

            this.ClientSize = new Size(850, 650);
            this.MinimumSize = new Size(850, 650);

            //Label[] userLabels = { userLabel1, userLabel2, userLabel3, userLabel4 };

            //for (int i = 0; i < userLabels.Length; i++)
            //{
            //    if (i < nicknames.Count)
            //        userLabels[i].Text = nicknames[i];
            //    else
            //        userLabels[i].Text = "";
            //}

           // CurrentTurnLabel.Text = nicknames.Count > 0 ? nicknames[0] + "( 흑 )" : "대기 중";

            _stream = stream;
            _myNickName = myNIckname;


          
            LoadChatForm(); // 외부 폼 로드
            LoadBoard(); // 바둑판 로드
            StartReceiveLoop();
        }

       

        /*******************
         *    로드 관련
         *******************/

        private void LoadChatForm()
        {
            string chatNickname = (userNicknames.Count > 0) ? userNicknames[0] : "Guest";
            chatForm = new ChatForm();
            ChattingPanel.Controls.Add(chatForm);
            chatForm.Dock = DockStyle.Fill;

            // chatForm.FormClosed += (s, args) => chatForm = null; // 폼이 닫히면 null로 설정

            // chatForm.StartPosition = FormStartPosition.Manual;
            //chatForm.Location = new Point(
            //    this.Location.X + this.Width,
            //    this.Location.Y
            //);
            // chatForm.Show();
            // SyncChatFormLocation(); // 위치 동기화
        }

        private void LoadBoard()
        {
            board = new GoBoardControl();
            board.Dock = DockStyle.Fill;

            board.NetStream = _stream;
            board.MyNick = _myNickName;
            board.MyId = _myID;

            MainPanel.Controls.Add(board);
            MainPanel.AutoScroll = false;
            MainPanel.HorizontalScrollbar = false;
            MainPanel.VerticalScrollbar = false;

            //CurrentTurnLabel.Text = userLabel1.Text + "( 흑 )";     // 시작은 user1 ( 흑 ) 차례 부터, 생성자에서 설정함

            board.CurrentTurnPlayer = null;
            CurrentTurnLabel.Text = "대기 중";

            board.OnTurnChanged += (turnText) =>                    // 턴이 바뀜에 따른 현재 턴 사용자 이름 출력
            {
                Debug.WriteLine("[DEBUG] TurnChanged 호출됨");

                //int totalPlayers = userNicknames.Count;
                //turnNum++;
                //if (turnNum > totalPlayers)
                //    turnNum = 1;

                //if (turnText == "게임 종료")
                //{
                //    CurrentTurnLabel.Text = "게임 종료";
                //    return;
                //}


                //if (turnNum - 1 < userNicknames.Count)
                //{
                //    board.CurrentTurnPlayer = userNicknames[turnNum - 1];
                //    CurrentTurnLabel.Text = userNicknames[turnNum - 1] + turnText;
                //}
                //else
                //{
                //    Debug.WriteLine("[ERROR] 유효하지 않은 턴 번호!");
                //}

                this.Invoke((Action)(() =>
                {
                    if (!string.IsNullOrEmpty(board.CurrentTurnPlayer))
                    {
                        CurrentTurnLabel.Text = $"{board.CurrentTurnPlayer} {turnText}";
                    }
                   
                }));

            };
           
        }

        private void StartReceiveLoop()
        {
            Debug.WriteLine("[DEBUG] ReceiveLoop 시작, stream=" + _stream);

            var buf = new byte[4096];
            new Thread(() =>
            {
                int count;
                while ((count = _stream.Read(buf, 0, buf.Length)) > 0)
                {
                    var p = (Packet)Packet.Deserialize(buf, 0, count);
                    switch (p.Type)
                    {
                        case (int)PacketType.접속자목록:
                            var ul = (UserListPacket)p;
                            // UI 스레드에서 라벨 업데이트
                            this.Invoke((Action)(() => UpdateUserLabels(ul.nicknames)));
                            break;

                        case (int)PacketType.PlacedStone:
                            var ps = (PlaceStonePacket)p;
                            this.Invoke((Action)(() => board.ApplyRemoteMove(ps)));
                            break;
                    }
                }
            })
            { IsBackground = true }.Start();
        }
        private void UpdateUserLabels(List<string> nicknames)
        {
            Label[] userLabels = { userLabel1, userLabel2, userLabel3, userLabel4 };
            for (int i = 0; i < userLabels.Length; i++)
            {
                userLabels[i].Text = i < nicknames.Count ? nicknames[i] : "";
            }
            if (!hasSetInitialTurn && nicknames.Count >= 2)
            {
                // 첫 번째 사용자(흑)를 CurrentTurnPlayer로 세팅
                board.CurrentTurnPlayer = nicknames[0];
                CurrentTurnLabel.Text = $"{nicknames[0]} ( 흑 )";
                hasSetInitialTurn = true;
            }
            else if (nicknames.Count < 2)
            {
                board.CurrentTurnPlayer = null;
                CurrentTurnLabel.Text = "대기 중";
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            // chatForm?.Close();
            // loginForm?.Show(); 필요 없음
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            // TODO : 팀 셔플 기능 구현
        }

        private void timeOutButton_Click(object sender, EventArgs e)
        {
            // TODO : 타임아웃 기능 구현
        }

        private void SkillButton3_Click(object sender, EventArgs e)
        {
            // TODO : 스킬3번 기능 구현
        }

        private void SkillButton2_Click(object sender, EventArgs e)
        {
            // TODO : 스킬2번 기능 구현
        }

        private void SkillButton1_Click(object sender, EventArgs e)
        {
            // TODO : 스킬1번 기능 구현
        }

        private void GameMainScreen_Load(object sender, EventArgs e)
        {
            // TODO : 로그인 된 유저들 이름으로 Label 이름 변경
            // userLabel1~4 의 Text 변경
        }

        private void MainPanel_Click(object sender, EventArgs e)
        {
            // TODO
            // 1. 패널 클릭 시 좌표 계산해서 착수
            // 2. 현재 턴 사용자 이름 CurrentTurnLabel.Text 변경
            // GoBoardControl에서 처리했음.
        }

        private void GameMainScreen_LocationChanged(object sender, EventArgs e)
        {
            SyncChatFormLocation();
        }

        private void SyncChatFormLocation()
        {
            if (chatForm != null && !chatForm.IsDisposed)
            {
                chatForm.Location = new Point(
                    this.Location.X + this.Width - 16,
                    this.Location.Y
                );
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
