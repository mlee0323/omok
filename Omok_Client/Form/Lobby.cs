using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Omok_Client.Network;
using Omok_Client.Util;

namespace Omok_Client.Form
{
    public partial class Lobby : MetroForm
    {
        public bool UserLoggedOut { get; private set; } = false;

        public Lobby()
        {
            InitializeComponent();
            this.FormClosing += Lobby_FormClosing;
            lbl_username.Text = Session.Nickname;
            UserLoggedOut = false;
        }

        // 방 생성
        private void btn_cr_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.");
                return;
            }

            Client.Send($"CREATE_ROOM|{Session.Pk}|{Session.Nickname}");
            string response = Client.Receive();

            if (response.StartsWith("JOIN_SUCCESS"))
            {
                string[] tokens = response.Split('|');
                string roomCode = tokens[1];
                Session.RoomCode = roomCode; // 세션에 방 코드 저장

                OpenGame(roomCode);
            }
            else
            {
                MessageBox.Show("방 생성 실패");
            }
        }

        private void btn_re_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.");
                return;
            }

            Client.Send($"JOIN_RANDOM|{Session.Pk}|{Session.Nickname}");
            string response = Client.Receive();

            if (response.StartsWith("JOIN_SUCCESS"))
            {
                string[] tokens = response.Split('|');
                string roomCode = tokens[1];
                OpenGame(roomCode);
            }
            else
            {
                MessageBox.Show("입장 가능한 방이 없습니다.");
            }
        }


        private void btn_ec_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.");
                return;
            }

            string inputCode = PromptRoomCode();
            if (string.IsNullOrWhiteSpace(inputCode))
                return;

            Client.Send($"JOIN_CODE|{Session.Pk}|{inputCode}");
            
            string response = Client.Receive();
            if (response.StartsWith("JOIN_SUCCESS"))
                OpenGame(inputCode);
            else
                MessageBox.Show("해당 방에 참가할 수 없습니다.");
        }

        private string PromptRoomCode()
        {
            var prompt = new MetroForm()
            {
                Width = 300,
                Height = 140,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                Resizable = false
            };

            var label = new MetroFramework.Controls.MetroLabel()
            {
                Text = "코드를 입력해주세요",
                Left = 20,
                Top = 20,
                Width = 260
            };

            var input = new MetroFramework.Controls.MetroTextBox()
            {
                Left = 20,
                Top = 50,
                Width = 260
            };

            var button1 = new MetroFramework.Controls.MetroButton()
            {
                Text = "확인",
                Left = 100,
                Top = 90,
                Width = 80,
                DialogResult = DialogResult.OK
            };

            var button2 = new MetroFramework.Controls.MetroButton()
            {
                Text = "닫기",
                Left = 200,
                Top = 90,
                Width = 80,
                DialogResult = DialogResult.OK
            };

            button1.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(label);
            prompt.Controls.Add(input);
            prompt.Controls.Add(button1);
            prompt.Controls.Add(button2);
            prompt.AcceptButton = button1;

            return prompt.ShowDialog() == DialogResult.OK ? input.Text.Trim() : null;
        }


        private void btn_history_Click(object sender, EventArgs e)
        {

        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 세션 초기화
            Session.Pk = 0;
            Session.Nickname = "";
            Session.RoomCode = "";

            UserLoggedOut = true;

            // 현재 Lobby 폼은 완전히 종료
            this.Close();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void OpenGame(string roomCode)
        {
            this.Hide();

            InGame ingame = new InGame(roomCode);
            ingame.Show();
            ingame.FormClosed += (s, args) => this.Show(); // 게임 종료 후 로비로 돌아옴
            
        }

        private void Lobby_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
