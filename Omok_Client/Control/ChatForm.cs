using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Omok_Client.Network;
using Omok_Client.Util;
using MetroFramework.Controls;

namespace Omok_Client.Control
{
    public partial class ChatForm : UserControl
    {
        public ChatForm()
        {
            InitializeComponent();
            txt_chat.ScrollBars = ScrollBars.Vertical;
            txt_chat.ReadOnly = true;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_msg.Text)) return;

            try
            {
                Client.Send($"CHAT|{Session.Pk}|{Session.Nickname}|{txt_msg.Text}");
                txt_msg.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메시지 전송 실패: {ex.Message}");
            }
        }

        private void txt_msg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btn_send_Click(sender, e);
            }
        }

        public void AppendSystemMessage(string message)
        {
            if (txt_chat.InvokeRequired)
            {
                txt_chat.Invoke(new Action(() => AppendSystemMessage(message)));
                return;
            }

            txt_chat.Text += $"[시스템] {message}{Environment.NewLine}";
        }

        public void AppendChatMessage(string nickname, string message)
        {
            if (txt_chat.InvokeRequired)
            {
                txt_chat.Invoke(new Action(() => AppendChatMessage(nickname, message)));
                return;
            }

            txt_chat.Text += $"{nickname}: {message}{Environment.NewLine}";
        }
    }
}
