using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using MetroFramework.Forms;

namespace Omok
{
    public partial class SignUpForm : MetroForm
    {
        private bool nickname_dupl_checked = false;
        private bool id_dupl_checked = false;
        private bool pw_checked = false;
        private bool pw2_checked = false;

        string userDBPath = Path.GetFullPath(Path.Combine("..", "..", "..", "db", "user.txt"));

        public SignUpForm()
        {
            InitializeComponent();

            signup_pw.UseSystemPasswordChar = true;
            signup_pw2.UseSystemPasswordChar = true;
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
            signup_btn.Enabled = false;
            nn_dupl_chk_btn.Enabled = false;
            id_dupl_chk_btn.Enabled = false;
        }

        private void nn_dupl_chk_btn_Click(object sender, EventArgs e)
        {
            // TODO: 닉네임 중복 체크 로직 추가
            if (!File.Exists(userDBPath))
            {
                MessageBox.Show("회원가입 > 닉네임 중복 체크 > DB 오류");
                return;
            }

            bool found = false;

            foreach (string line in File.ReadLines(userDBPath))
            {
                string[] tokens = line.Split(',');
                if (tokens.Length >= 2)
                {
                    string nickname = tokens[1];
                    if (signup_name.Text == nickname)
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("사용 가능한 닉네임입니다.");
                nickname_dupl_checked = true;
            }
            else
                MessageBox.Show("이미 존재하는 닉네임입니다.");

            validateForm();
        }

        private void id_dupl_chk_btn_Click(object sender, EventArgs e)
        {
            // TODO: 아이디 중복 체크 로직 추가
            if (!File.Exists(userDBPath))
            {
                MessageBox.Show("회원가입 > 아이디 중복 체크 > DB 오류");
                return;
            }

            bool found = false;

            foreach (string line in File.ReadLines(userDBPath))
            {
                string[] tokens = line.Split(',');
                if (tokens.Length >= 2)
                {
                    string id = tokens[2];
                    if (signup_id.Text == id)
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("사용 가능한 유저명입니다.");
                id_dupl_checked = true;
            } 
            else
                MessageBox.Show("이미 존재하는 유저명입니다.");

            validateForm();
        }

        private void signup_name_TextChanged(object sender, EventArgs e)
        {
            nickname_dupl_checked = false;
            nn_dupl_chk_btn.Enabled = false;
            if (signup_name.Text.Length > 0)
                nn_dupl_chk_btn.Enabled = true;

            validateForm();
        }

        private void signup_id_TextChanged(object sender, EventArgs e)
        {
            id_dupl_checked = false;
            id_dupl_chk_btn.Enabled = false;
            if (signup_id.Text.Length > 0)
                id_dupl_chk_btn.Enabled = true;
            
            validateForm();
        }

        private void signup_pw_TextChange(object sender, EventArgs e)
        {
            if (signup_pw.Text.Length < 4 || signup_pw.Text.Length > 10)
            {
                signup_pw_warning_lb.Text = "비밀번호는 4 ~ 10자 이내로 작성해야 합니다.";
                signup_pw_warning_lb.ForeColor = Color.Red;
                pw_checked = false;
            }
            else
            {
                signup_pw_warning_lb.Text = "이 비밀번호는 사용 가능합니다.";
                signup_pw_warning_lb.ForeColor = Color.Green;
                pw_checked = true;
            }
            signup_pw2_TextChange(sender, null);
            validateForm();
        }

        private void signup_pw2_TextChange(object sender, EventArgs e)
        {
            if (signup_pw.Text == "")
                return;

            if (signup_pw.Text != signup_pw2.Text)
            {
                signup_pw2_warning_lb.Text = "비밀번호가 일치하지 않습니다.";
                signup_pw2_warning_lb.ForeColor = Color.Red;
                pw2_checked = false;
            }
            else
            {
                signup_pw2_warning_lb.Text = "비밀번호가 일치합니다.";
                signup_pw2_warning_lb.ForeColor = Color.Green;
                pw2_checked = true;
            }
            validateForm();
        }

        private void validateForm()
        {
            if (nickname_dupl_checked && id_dupl_checked && pw_checked && pw2_checked)
            {
                signup_btn.Enabled = true;
            }
            else
            {
                signup_btn.Enabled = false;
            }
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(userDBPath))
            {
                MessageBox.Show("회원가입 > 회원가입 > DB 오류");
                return;
            }

            string content = File.ReadAllText(userDBPath);
            bool needsLineBreak = !string.IsNullOrEmpty(content) &&
                                  !content.EndsWith("\n") && !content.EndsWith("\r");

            int lineNumber = File.ReadAllLines(userDBPath).Length + 1;

            using (StreamWriter sw = new StreamWriter(userDBPath, true))
            {
                if (needsLineBreak)
                    sw.WriteLine();  // 단순 개행 한 번

                sw.WriteLine($"{lineNumber},{signup_name.Text},{signup_id.Text},{signup_pw.Text}");  // 개행 자동 포함
            }

            MessageBox.Show("회원가입되었습니다.");
            this.Close();
        }


        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
