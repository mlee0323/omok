using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using MetroFramework.Forms;
using Omok;
using Pack_Server;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Omok_Server;
using System.Collections.Generic;

namespace Omok_Client
{
    public partial class Login : MetroForm
    {
        private TcpClient m_client;
        private NetworkStream m_stream;
        private Thread m_recvThread;

        private Form1 serverForm;

        public static List<string> loggedInUsers = new List<string>();

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
            if (login_id.Text.Length > 0 && login_pw.Text.Length > 0 && login_ip.Text.Length > 0 )
                login_btn.Enabled = true;
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string ip = login_ip.Text.Trim();         // 서버 IP
            int port = 9999;    // 포트 번호

            

            try
            {
                m_client = new TcpClient();
                m_client.Connect(ip, port); // 서버에 연결
                m_stream = m_client.GetStream();


                if (!File.Exists(userDBPath))
                {
                    MessageBox.Show("로그인 > DB 오류");
                    return;
                }

                string userNickname = null;

                foreach (string line in File.ReadLines(userDBPath))
                {
                    string[] tokens = line.Split(',');
                    if (tokens.Length < 4)
                        continue;

                    string nickname = tokens[1];
                    string id = tokens[2];
                    string pw = tokens[3];

                    if (login_id.Text == id && login_pw.Text == pw)
                    {
                        userNickname = nickname;
                        break;
                    }
                }


                // 로그인 패킷 생성
                Login_server loginPacket = new Login_server();
                loginPacket.Type = (int)PacketType.로그인;
                loginPacket.m_strID = login_id.Text;
               

                byte[] data = Packet.Serialize(loginPacket);
                m_stream.Write(data, 0, data.Length);


                if (userNickname == null)
                {
                    MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");
                    return; // 로그인 실패
                }

                if (!Login.loggedInUsers.Contains(userNickname))
                    Login.loggedInUsers.Add(userNickname);

                // 바둑판용 패킷
                TcpClient gameClient = new TcpClient();
                gameClient.Connect(ip, port);  
                NetworkStream gameStream = gameClient.GetStream();

                //GameMainScreen = new GameMainScreen(Login.loggedInUsers);
                GameMainScreen = new GameMainScreen(
                    Login.loggedInUsers,
                    gameStream,
                    userNickname, login_id.Text);
                GameMainScreen.FormClosed += (s, ev) =>
                {
                    this.Show();       // 로그인 폼 다시 보이기
                };

                GameMainScreen.Show();
                this.Hide();
            } catch (Exception ex)
            {
                MessageBox.Show("서버 접속 실패: " + ex.Message);
            }


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

        private void btn_Server_Open_Click(object sender, EventArgs e)
        {
            // 1. 서버 폼 띄우기 + 서버 시작
            if (serverForm == null || serverForm.IsDisposed)
            {
                serverForm = new Form1();
                serverForm.Show();
                serverForm.StartServer();
            }

            // 2. 127.0.0.1로 로그인 자동 시도
            string ip = "127.0.0.1";
            int port = 9999;

            try
            {
                m_client = new TcpClient();
                m_client.Connect(ip, port); // 서버에 연결
                m_stream = m_client.GetStream();

                // 로그인 패킷 생성
                Login_server loginPacket = new Login_server();
                loginPacket.Type = (int)PacketType.로그인;
                loginPacket.m_strID = login_id.Text;

                byte[] data = Packet.Serialize(loginPacket);
                m_stream.Write(data, 0, data.Length);

                if (!File.Exists(userDBPath))
                {
                    MessageBox.Show("로그인 > DB 오류");
                    return;
                }

                string userNickname = null;

                foreach (string line in File.ReadLines(userDBPath))
                {
                    string[] tokens = line.Split(',');
                    if (tokens.Length < 4)
                        continue;

                    string nickname = tokens[1];
                    string id = tokens[2];
                    string pw = tokens[3];

                    if (login_id.Text == id && login_pw.Text == pw)
                    {
                        userNickname = nickname;
                        break;
                    }
                }

                if (userNickname == null)
                {
                    MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");

                    return; // 로그인 실패
                }

                // 로그인 성공, 로그인 유저 리스트에 추가 (중복 방지)
                if (!Login.loggedInUsers.Contains(userNickname))
                {
                    Login.loggedInUsers.Add(userNickname);
                }

                // 게임 메인 화면 생성 시 로그인 유저 리스트 전달
                //GameMainScreen = new GameMainScreen(Login.loggedInUsers);

                // 바둑판용 패킷
                TcpClient gameClient = new TcpClient();
                gameClient.Connect(ip, port);  
                NetworkStream gameStream = gameClient.GetStream();

                GameMainScreen = new GameMainScreen(
                    Login.loggedInUsers,
                    gameStream,
                    userNickname,
                    login_id.Text

                );

                GameMainScreen.FormClosed += (s, ev) =>
                {
                    this.Show();  // 로그인 폼 다시 보이기
                };

                GameMainScreen.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 접속 실패: " + ex.Message);
            }

        }
    }
}
