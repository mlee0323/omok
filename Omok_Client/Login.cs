using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using MetroFramework.Forms;
using Omok;

namespace Omok_Client
{
    public partial class Login : MetroForm
    {
        string userDBPath = Path.GetFullPath(Path.Combine("..", "..", "..", "db", "user.txt"));

        public ChatForm ChatForm;
        public GameMainScreen GameMainScreen;

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
            GameMainScreen = new GameMainScreen();
            GameMainScreen.FormClosed += (s, ev) =>
            {
                this.Show();       // 로그인 폼 다시 보이기
            };

            GameMainScreen.Show();
            this.Hide();

            //if (!File.Exists(userDBPath))
            //{
            //    MessageBox.Show("로그인 > DB 오류");
            //    return;
            //}

            //bool found = false;

            //foreach (string line in File.ReadLines(userDBPath))
            //{
            //    int id = 0;
            //    string nickname = "";
            //    string[] tokens = line.Split(',');
            //    if (tokens.Length >= 2)
            //    {
            //        id = int.Parse(tokens[0]);
            //        nickname = tokens[1];
            //        string userid = tokens[2];
            //        string pw = tokens[3];
            //        if (login_id.Text == userid && login_pw.Text == pw)
            //        {
            //            found = true;
            //            break;
            //        }
            //    }
            //}

            //if (!found)
            //{
            //    MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");
            //}
            //else
            //{
            //    // TODO: 로그인 성공 후 처리
            //    // 게임 대기 화면 Form으로 이동 코드 필요


            //}
        }




        private void signup_btn_Click(object sender, EventArgs e)
        {
            SignUpForm signupForm = new SignUpForm();
            signupForm.ShowDialog();
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
