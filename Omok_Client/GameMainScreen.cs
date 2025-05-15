using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;

namespace Omok
{
    public partial class GameMainScreen : MetroForm
    {
        private ChatForm chatForm;

        private int turnNum = 1;

        public GameMainScreen()
        {
            InitializeComponent();

            this.ClientSize = new Size(850, 650);
            this.MinimumSize = new Size(850, 650);

            LoadChatForm(); // 외부 폼 로드
            LoadBoard(); // 바둑판 로드
        }

        /*******************
         *    로드 관련
         *******************/

        private void LoadChatForm()
        {
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
            GoBoardControl board = new GoBoardControl();
            board.Dock = DockStyle.Fill;

            MainPanel.Controls.Add(board);
            MainPanel.AutoScroll = false;
            MainPanel.HorizontalScrollbar = false;
            MainPanel.VerticalScrollbar = false;

            CurrentTurnLabel.Text = userLabel1.Text + "( 흑 )";     // 시작은 user1 ( 흑 ) 차례 부터

            board.OnTurnChanged += (turnText) =>                    // 턴이 바뀜에 따른 현재 턴 사용자 이름 출력
            {
                turnNum++;
                if (turnNum == 5)
                    turnNum = 1;
                if (turnText == "게임 종료")                        // turnText : 현재 차례 표시 ( 흑 / 백 )
                {
                    CurrentTurnLabel.Text = "게임 종료";
                    return;
                }

                switch (turnNum)                                    // turnNum : 현재 유저 차례 번호
                {
                    case 1:
                        turnText = userLabel1.Text + turnText;                       
                        break;
                    case 2:
                        turnText = userLabel2.Text + turnText;                        
                        break;
                    case 3:
                        turnText = userLabel3.Text + turnText;                       
                        break;
                    case 4:
                        turnText = userLabel4.Text + turnText;                      
                        break;
                }
                
                CurrentTurnLabel.Text = turnText;
                
            };

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
