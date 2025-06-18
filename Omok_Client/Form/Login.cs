using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using Omok_Client.Network;
using Omok_Client.Util;

namespace Omok_Client.Form
{
    public partial class Login : MetroForm
    {
        public static List<string> loggedInUsers = new List<string>();

        public InGame GameMainScreen;

        public Login()
        {
            InitializeComponent();
            login_pw.UseSystemPasswordChar = true;
        }
            
        private void Login_Load(object sender, EventArgs e)
        {
            login_btn.Enabled = false;
        }

        private void login_TextChanged(object sender, EventArgs e)
        {
            login_btn.Enabled = false;
            if (login_id.Text.Length > 0 && login_pw.Text.Length > 0)
                login_btn.Enabled = true;
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (!Client.Connect())
            {
                MessageBox.Show("서버 연결 실패");
                return;
            }

            string id = login_id.Text.Trim();
            string pw = login_pw.Text.Trim();
            Client.Send($"LOGIN|{id}|{pw}");

            string response = Client.Receive();
            if (response.StartsWith("LOGIN_SUCCESS"))
            {
                string[] tokens = response.Split('|');
                int pk = int.Parse(tokens[1]);
                string nickname = tokens[2];

                Session.Pk = pk;
                Session.Nickname = nickname;

                Lobby lobby = new Lobby();
                lobby.FormClosed += (s, e) =>
                {
                    if (lobby.UserLoggedOut)
                    {
                        this.Show(); // 로그아웃이면 다시 로그인 폼 보여줌
                    }
                    else
                    {
                        this.Close(); // 나가기 버튼 눌러 종료한 경우 프로그램 종료
                    }
                };

                this.Hide();
                lobby.Show();

            }
            else
            {
                MessageBox.Show("로그인 실패");
            }
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            SignUp signupForm = new SignUp();
            signupForm.ShowDialog();
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
