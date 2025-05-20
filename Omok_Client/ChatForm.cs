using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pack_Server;

namespace Omok
{
    public partial class ChatForm : UserControl
    {
        private string nickname;

        // 서버 연결용 변수
        private TcpClient client;
        private NetworkStream stream;

        public ChatForm() : this("Guest") { }

        // 닉네임 지정 생성자
        public ChatForm(string nickname)
        {
            InitializeComponent();
            this.nickname = nickname;

            // 디자이너 실행 시 서버 연결하지 않도록 예외 처리
            if (!this.DesignMode)
            {
                ConnectToServer("127.0.0.1", 9999);
            }
        }

        // 서버 연결
        private void ConnectToServer(string ip, int port)
        {
            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();

                // 메시지 수신을 위한 스레드 시작
                Thread receiveThread = new Thread(Receive);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                Message("서버에 연결되었습니다.");
            }
            catch (Exception ex)
            {
                Message("서버 연결 실패: " + ex.Message);
            }
        }

        // 메시지 출력
        public void Message(string msg)
        {
            if (txt_all.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    txt_all.AppendText(msg + "\r\n");
                    txt_all.Focus();
                    txt_all.ScrollToCaret();
                    txt_Send.Focus();
                }));
            }
            else
            {
                txt_all.AppendText(msg + "\r\n");
                txt_all.Focus();
                txt_all.ScrollToCaret();
                txt_Send.Focus();
            }
        }

        // 메시지 수신
        public void Receive()
        {
            try
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    // 패킷 역직렬화
                    Packet packet = (Packet)Packet.Deserialize(buffer, 0, bytesRead);

                    if (packet.Type == (int)PacketType.채팅메시지)
                    {
                        ChatMessage chat = (ChatMessage)packet;
                        // 받은 메시지는 상대방 닉네임: 메시지 로 표시
                        Message($"{chat.sender}: {chat.message}");
                    }
                    else
                    {
                        // 다른 타입 메시지 처리 또는 로그용
                        Message($"[서버] {packet.Type} 패킷 수신");
                    }
                }
            }
            catch (Exception ex)
            {
                Message("서버와 연결이 끊겼습니다: " + ex.Message);
            }
        }


        // 메시지 전송
        public void Send()
        {
            if (string.IsNullOrWhiteSpace(txt_Send.Text))
                return;

            ChatMessage chatPacket = new ChatMessage();
            chatPacket.Type = (int)PacketType.채팅메시지;
            chatPacket.sender = nickname;  // 내 닉네임
            chatPacket.message = txt_Send.Text;

            byte[] data = Packet.Serialize(chatPacket);

            try
            {
                stream.Write(data, 0, data.Length);
                // 내가 보낸 메시지는 '나:'로 표시
                Message($"나: {txt_Send.Text}");
                txt_Send.Clear();
            }
            catch (Exception ex)
            {
                Message("메시지 전송 실패: " + ex.Message);
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void txt_Send_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

    }
}
