using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Omok_Server2
{
    public partial class ServerForm : MetroForm
    {
        private TcpListener listener;
        private Thread listenThread;
        private bool isRunning = false;
        private List<ClientHandler> clients = new List<ClientHandler>();

        public ServerForm()
        {
            InitializeComponent();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                listenThread = new Thread(StartServer);
                listenThread.IsBackground = true;
                listenThread.Start();
                isRunning = true;
                AppendLog("서버 시작됨");
                btn_server.Enabled = false;
            }
        }

        private void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 9999);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                AppendLog("클라이언트 연결됨");

                ClientHandler handler = new ClientHandler(client, AppendLog);
                clients.Add(handler);

                Thread t = new Thread(handler.Process);
                t.IsBackground = true;
                t.Start();

                AddThreadToList(t);
            }
        }

        public void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => AppendLog(message)));
            }
            else
            {
                txtLog.AppendText("[" + DateTime.Now + "] " + message + Environment.NewLine);
            }
        }

        private void AddThreadToList(Thread t)
        {
            //if (threadList.InvokeRequired)
            //{
            //    threadList.Invoke(new Action(() => AddThreadToList(t)));
            //}
            //else
            //{
            //    ListViewItem item = new ListViewItem(t.Name ?? $"Thread {t.ManagedThreadId}");
            //    item.SubItems.Add(t.ThreadState.ToString());
            //    threadList.Items.Add(item);
            //}
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
