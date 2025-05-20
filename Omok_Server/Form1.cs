using System;
using System.Windows.Forms;

namespace Omok_Server
{
    public partial class Form1 : Form
    {
        private ServerManager serverManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (serverManager == null)
            {
                serverManager = new ServerManager(this, 9999);
                serverManager.Start();
                LogMessage("서버 시작됨");
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            if (serverManager != null)
            {
                serverManager.Stop();
                serverManager = null;
                LogMessage("서버 중지됨");
            }
        }

        // 로그 메시지 UI 출력용 (예: 텍스트박스에 로그 남기기)
        public void LogMessage(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    txtLog.AppendText(msg + Environment.NewLine);
                }));
            }
            else
            {
                txtLog.AppendText(msg + Environment.NewLine);
            }
        }

        public void StartServer()
        {
            if (serverManager == null)
            {
                serverManager = new ServerManager(this, 9999);
                serverManager.Start();
                LogMessage("서버 시작됨 (외부에서 실행됨)");
            }
        }

    }
}
