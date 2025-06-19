using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Omok_Client.Network;

namespace Omok_Client.Form
{
    public partial class SignUp : MetroForm
    {
        private bool nn_dupl_checked = false;
        private bool id_dupl_checked = false;
        private bool pw_checked = false;
        private bool pw2_checked = false;

        public SignUp()
        {
            InitializeComponent();
            signup_pw.UseSystemPasswordChar = true;
            signup_pw2.UseSystemPasswordChar = true;
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
            signup_btn.Enabled = false;
            nn_dupl_chk_btn.Enabled = false;
            un_dupl_chk_btn.Enabled = false;
        }

        private void nn_dupl_chk_btn_Click(object sender, EventArgs e)
        {
            if (signup_name == null || string.IsNullOrWhiteSpace(signup_name.Text))
            {
                MessageBox.Show("닉네임을 입력해주세요.");
                signup_name.Focus();
                return;
            }

            un_dupl_chk_btn.Enabled = false; // 중복 클릭 방지

            try
            {
                if (!Client.Connect())
                    throw new Exception("서버 연결 실패");

                Client.Send($"CHECK_NICK|{signup_name.Text.Trim()}");
                string response = Client.Receive();

                if (response == "NICK_OK")
                    nn_dupl_checked = true;
                else
                    nn_dupl_checked = false;

                MessageBox.Show(response == "NICK_OK" ? "사용 가능한 닉네임" : "이미 존재하는 닉네임");
                Client.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                signup_name.Focus(); // 다시 포커싱
                nn_dupl_chk_btn.Enabled = true;
                validateForm();
            }  
        }

        private void un_dupl_chk_btn_Click(object sender, EventArgs e)
        {
            if (signup_id == null || string.IsNullOrWhiteSpace(signup_id.Text))
            {
                MessageBox.Show("아이디를 입력해주세요.");
                signup_id.Focus();
                return;
            }

            un_dupl_chk_btn.Enabled = false;
            try
            {
                if (!Client.Connect())
                    throw new Exception("서버 연결 실패");

                Client.Send($"CHECK_ID|{signup_id.Text.Trim()}");
                string response = Client.Receive();

                if (response == "ID_OK")
                    id_dupl_checked = true;
                else
                    id_dupl_checked = false;

                MessageBox.Show(response == "ID_OK" ? "사용 가능한 아이디" : "이미 존재하는 아이디");
                Client.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                signup_id.Focus(); // 다시 포커싱
                un_dupl_chk_btn.Enabled = true;
                validateForm();
            }
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            string nickname = signup_name.Text.Trim();
            string id = signup_id.Text.Trim();
            string pw = signup_pw.Text.Trim();
            string pw2 = signup_pw2.Text.Trim();

            if (pw != pw2)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            if (pw.Length < 5 || pw.Length > 15)
            {
                MessageBox.Show("비밀번호는 5자 이상 15자 이하로 설정하세요.");
                return;
            }

            if (!Client.Connect())
            {
                MessageBox.Show("서버 연결 실패");
                return;
            }

            Client.Send($"REGISTER|{id}|{pw}|{nickname}");
            string response = Client.Receive();

            if (response.StartsWith("REGISTER_SUCCESS"))
            {
                MessageBox.Show("회원가입 성공!");
                this.Close(); // or this.Hide();
            }
            else
            {
                MessageBox.Show("회원가입 실패 (아이디 또는 닉네임 중복)");
            }

            Client.Disconnect();
        }

        private void signup_name_TextChanged(object sender, EventArgs e)
        {
            nn_dupl_checked = false;
            nn_dupl_chk_btn.Enabled = signup_name.Text.Length > 0;
            validateForm();
        }

        private void signup_id_TextChanged(object sender, EventArgs e)
        {
            id_dupl_checked = false;
            un_dupl_chk_btn.Enabled = signup_id.Text.Length > 0;
            validateForm();
        }

        private void signup_pw_TextChange(object sender, EventArgs e)
        {
            if (signup_pw.Text.Length < 4 || signup_pw.Text.Length > 15)
            {
                signup_pw_warning_lb.Text = "비밀번호는 4 ~ 15자 이내로 작성해야 합니다.";
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
            if (string.IsNullOrEmpty(signup_pw.Text))
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
            signup_btn.Enabled = nn_dupl_checked && id_dupl_checked && pw_checked && pw2_checked;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
