using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok_Chat
{
    public partial class Form1 : Form
    {
        public NetworkStream m_Stream;
        public StreamReader m_Read;
        public StreamWriter m_Write;
        const int PORT = 2002;
        private Thread m_ThReader;

        public bool m_bStop = false;

        private TcpListener m_listener;
        private Thread m_thServer;

        public bool m_bConnect = false;
        TcpClient m_Client;

        private CancellationTokenSource cancellationTokenSource1;
        private CancellationToken cancellationToken1;
        private CancellationTokenSource cancellationTokenSource2;
        private CancellationToken cancellationToken2;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerStop();
            Disconnect();

        }
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        public void Message(string msg)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                txt_all.AppendText(msg + "\r\n");
                txt_all.Focus();
                txt_all.ScrollToCaret();
                txt_Send.Focus();
            }));

        }

        public void ServerStart()
        {
            try
            {
                m_listener = new TcpListener(PORT);
                m_listener.Start();

                m_bConnect = true;
                Message("클라이언트 접속 대기중");

                while (m_bConnect)
                {
                    TcpClient hClent = m_listener.AcceptTcpClient();

                    if (hClent.Connected)
                    {
                        m_bConnect = true;
                        Message("클라이언트 접속");

                        m_Stream = hClent.GetStream();
                        m_Read = new StreamReader(m_Stream);
                        m_Write = new StreamWriter(m_Stream);

                        m_ThReader = new Thread(new ThreadStart(Receive));
                        m_ThReader.Start();

                    }
                }
            }
            catch
            {
                Message("시작 도중에 오류 발생");
                return;
            }
        }
        public void ServerStop()
        {
            if (!m_bStop)
                return;

            m_listener.Stop();
            m_Read.Close();
            m_Write.Close();

            m_Stream.Close();

            m_ThReader.Abort();
            m_thServer.Abort();

            Message("서비스 종료");

        }
        public void Disconnect()
        {
            if (!m_bConnect)
                return;

            m_bConnect = false;
            m_Read.Close();
            m_Write.Close();

            m_Stream.Close();
            m_ThReader.Abort();
            Message("상대방과 연결 중단");

        }
        public void Connect()
        {
            m_Client = new TcpClient();

            try
            {
                m_Client.Connect(txt_ServerIp.Text, PORT);
            }
            catch
            {
                m_bConnect = false;
                return;
            }
            m_bConnect = true;
            Message("서버에 연결");

            m_Stream = m_Client.GetStream();

            m_Read = new StreamReader(m_Stream);
            m_Write = new StreamWriter(m_Stream);

            m_ThReader = new Thread(new ThreadStart(Receive));
            m_ThReader.Start();
        }
        public void Receive()
        {
            try
            {
                while (m_bConnect)
                {
                    string szMessage = m_Read.ReadLine();

                    if (szMessage != null)
                        Message("상대방 >>> :" + szMessage);
                }
            }
            catch
            {
                Message("데이터를 읽는 과정에서 오류가 발생");

            }
            Disconnect();

        }
        public void Send()
        {
            try
            {
                m_Write.WriteLine(txt_Send.Text);
                m_Write.Flush();

                Message(">>> : " + txt_Send.Text);
                txt_Send.Text = "";

            }
            catch
            {
                Message("데이터 전송 실패");
            }
        }

        private void btn_Server_Click(object sender, EventArgs e)
        {
            if (btn_Server.Text == "서버 켜기")
            {
                m_thServer = new Thread(new ThreadStart(ServerStart));
                m_thServer.Start();

                btn_Server.Text = "서버 멈춤";
                btn_Server.ForeColor = Color.Red;
            }
            else
            {
                ServerStop();
                btn_Server.Text = "서버 켜기";
                btn_Server.ForeColor = Color.Black;
            }
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (btn_Connect.Text == "서버 연결")
            {
                Connect();
                if (m_bConnect)
                {
                    btn_Connect.Text = "서버 연결 끊기";
                    btn_Connect.ForeColor = Color.Red;
                }
                
            }
            else
            {
                Disconnect();
                btn_Connect.Text = "서버 연결";
                btn_Connect.ForeColor = Color.Black;
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void txt_Send_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Send();
            }
        }

        private void btn_emoji_Click(object sender, EventArgs e)
        {
            ContextMenu emojiMenu = new ContextMenu();
            emojiMenu.MenuItems.Add("😊", (s, args) => InsertEmoji("😊"));
            emojiMenu.MenuItems.Add("😂", (s, args) => InsertEmoji("😂"));
            emojiMenu.MenuItems.Add("❤️", (s, args) => InsertEmoji("❤️"));
            emojiMenu.MenuItems.Add("👍", (s, args) => InsertEmoji("👍"));
            emojiMenu.Show(btn_emoji, new Point(0, btn_emoji.Height));
        }

        private void InsertEmoji(string emoji)
        {
            txt_Send.AppendText(emoji);
            txt_Send.Focus();
        }
    }

}
