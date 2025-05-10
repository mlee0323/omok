using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok
{
    public partial class GameMainScreen : Form
    {
        private Form loginForm;
        private Form chatForm;
        public GameMainScreen(Form loginForm,Form chatForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.chatForm = chatForm;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            chatForm?.Close();
            loginForm?.Show();
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
    }
}
