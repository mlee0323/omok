using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Omok_Network;

namespace Omok
{
    public partial class SignUpForm : MetroForm
    {
        private bool nickname_dupl_checked = false;
        private bool id_dupl_checked = false;
        private bool pw_checked = false;
        private bool pw2_checked = false;

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

        private async void nn_dupl_chk_btn_Click(object sender, EventArgs e)
        {
            if (signup_name == null || string.IsNullOrWhiteSpace(signup_name.Text))
            {
                MessageBox.Show("닉네임을 입력해주세요.");
                signup_name.Focus();
                return;
            }

            nn_dupl_chk_btn.Enabled = false; // 중복 클릭 방지

            var packet = new Authenticate_Packet
            {
                command = "NAME_CHECK",
                nickname = signup_name.Text
            };

            Packet responsePacket = await Task.Run(() => NetworkManager.SendPacketSync(packet));

            if (responsePacket is Authenticate_Packet authResult)
            {
                string result = authResult.result;

                if (result == "NAME_DUPLICATE")
                {
                    MessageBox.Show("이미 존재하는 닉네임입니다.");
                }
                else if (result == "NAME_OK")
                {
                    MessageBox.Show("사용 가능한 닉네임입니다.");
                    nickname_dupl_checked = true;
                }
                else
                {
                    MessageBox.Show("서버 응답 오류: " + result);
                }
            }
            else
            {
                MessageBox.Show("서버에서 받은 패킷은 Authenticate_Packet이 아닙니다.\n" +
                                $"받은 타입: {responsePacket?.GetType().FullName ?? "null"}");
            }

            signup_name.Focus(); // 다시 포커싱
            nn_dupl_chk_btn.Enabled = true;
            validateForm();
        }

        private void id_dupl_chk_btn_Click(object sender, EventArgs e)
        {
            var packet = new Authenticate_Packet
            {
                command = "ID_CHECK",
                username = signup_id.Text
            };

            var responsePacket = NetworkManager.SendPacketSync(packet);
            if (responsePacket is Authenticate_Packet authResult)
            {
                string result = authResult.result;

                if (result == "ID_DUPLICATE")
                {
                    MessageBox.Show("이미 존재하는 아이디입니다.");
                }
                else if (result == "ID_OK")
                {
                    MessageBox.Show("사용 가능한 아이디입니다.");
                    id_dupl_checked = true;
                }
                else
                {
                    MessageBox.Show("서버 응답 오류: " + result + $" (command: {packet.command})");
                }
            }
            else
            {
                MessageBox.Show("잘못된 응답 형식입니다.");
            }

            validateForm();
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            var packet = new Authenticate_Packet
            {
                command = "REGISTER",
                nickname = signup_name.Text,
                username = signup_id.Text,
                password = signup_pw.Text,
                confirm = signup_pw2.Text
            };

            var responsePacket = NetworkManager.SendPacketSync(packet);
            if (responsePacket is Authenticate_Packet authResult)
            {
                string result = authResult.result;

                if (result == "REGISTER_SUCCESS")
                {
                    MessageBox.Show("회원가입되었습니다.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("회원가입 실패: " + result);
                }
            }
            else
            {
                MessageBox.Show("서버 응답 오류 또는 잘못된 응답 형식입니다.");
            }
        }

        private void signup_name_TextChanged(object sender, EventArgs e)
        {
            nickname_dupl_checked = false;
            nn_dupl_chk_btn.Enabled = signup_name.Text.Length > 0;
            validateForm();
        }

        private void signup_id_TextChanged(object sender, EventArgs e)
        {
            id_dupl_checked = false;
            id_dupl_chk_btn.Enabled = signup_id.Text.Length > 0;
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
            signup_btn.Enabled = nickname_dupl_checked && id_dupl_checked && pw_checked && pw2_checked;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
