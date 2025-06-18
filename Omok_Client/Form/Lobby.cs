using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
            
            // 폼이 렌더링된 뒤 승무패 기록 갱신
            this.Shown += (_, __) => UpdateRecordWithNewSocket();
        }

        // 방 생성
        private void btn_cr_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string response = Client.Request($"CREATE_ROOM|{Session.Pk}|{Session.Nickname}");
                if (response.StartsWith("JOIN_SUCCESS|"))
                {
                    var roomCode = response.Split('|')[1];
                    Session.RoomCode = roomCode;
                    OpenGame(roomCode);
                }
                else
                {
                    MessageBox.Show($"방 생성 실패: {response}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"네트워크 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 랜덤 참가
        private void btn_re_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.");
                return;
            }

            string response = Client.Request($"JOIN_RANDOM|{Session.Pk}|{Session.Nickname}");
            if (response.StartsWith("JOIN_SUCCESS"))
            {
                string roomCode = response.Split('|')[1];
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
            if (string.IsNullOrWhiteSpace(inputCode)) return;

            string response = Client.Request($"JOIN_CODE|{Session.Pk}|{inputCode}");
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
            var historyForm = new History();
            historyForm.Show(); // Modaless로 Form 표시
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
            this.Hide();

            // 세션 초기화
            Session.Pk = 0;
            Session.Nickname = "";
            Session.RoomCode = "";

            this.Close();

            Application.Exit();
        }

        private void OpenGame(string roomCode)
        {
            this.Hide();
            var ingame = new InGame(roomCode);
            ingame.Show();
            ingame.FormClosed += (s, args) =>
            {
                UpdateRecordWithNewSocket();
                this.Show();
            };
        }

        private void UpdateRecordWithNewSocket()
        {
            lbl_win.Text = lbl_draw.Text = lbl_lose.Text = "0";
            try
            {
                using (var hist = new TcpClient(Client.GetHost(), Client.GetPort()))
                using (var writer = new StreamWriter(hist.GetStream(), Encoding.UTF8) { AutoFlush = true })
                using (var reader = new StreamReader(hist.GetStream(), Encoding.UTF8))
                {
                    writer.WriteLine($"HISTORY_LOAD|{Session.Pk}");
                    string msg;
                    int w = 0, d = 0, l = 0;
                    while ((msg = reader.ReadLine()) != null)
                    {
                        if (msg == "HISTORY_LOAD_END") break;
                        if (!msg.StartsWith("HISTORY_GAME|")) continue;

                        var t = msg.Split('|');
                        if (t.Length >= 6)
                        {
                            switch (t[5][0])
                            {
                                case 'W': w++; break;
                                case 'D': d++; break;
                                case 'L': l++; break;
                            }
                        }
                    }
                    lbl_win.Text = w.ToString();
                    lbl_draw.Text = d.ToString();
                    lbl_lose.Text = l.ToString();
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"히스토리 조회 중 네트워크 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Lobby_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit(); // 애플리케이션 종료
        }
    }
}
