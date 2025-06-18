using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing;
using Omok_Client.Network;
using Omok_Client.Util;
using Omok_Client.Form;
using MetroFramework.Controls;

namespace Omok_Client.Control
{
    public partial class ChatForm : UserControl
    {
        public ChatForm()
        {
            InitializeComponent();
            txt_chat.ReadOnly = true;
            
            // btn_emj 버튼에 클릭 이벤트 추가
            btn_emj.Click += btn_emj_Click;
        }

        private void btn_emj_Click(object sender, EventArgs e)
        {
            try
            {
                using (EmojiForm emojiForm = new EmojiForm())
                {
                    if (emojiForm.ShowDialog() == DialogResult.OK)
                    {
                        // 선택된 이모지를 채팅으로 전송
                        string selectedEmoji = emojiForm.SelectedEmoji;
                        if (!string.IsNullOrEmpty(selectedEmoji))
                        {
                            // 이모지 전송 (특별한 형식으로 전송)
                            Client.Send($"EMOJI|{Session.Pk}|{Session.Nickname}|{selectedEmoji}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이모지 전송 실패: {ex.Message}");
            }
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

            txt_chat.SelectionStart = txt_chat.TextLength;
            txt_chat.SelectionColor = Color.Gray;
            txt_chat.AppendText($"[시스템] {message}{Environment.NewLine}");
        }

        public void AppendChatMessage(string nickname, string message)
        {
            if (txt_chat.InvokeRequired)
            {
                txt_chat.Invoke(new Action(() => AppendChatMessage(nickname, message)));
                return;
            }

            txt_chat.SelectionStart = txt_chat.TextLength;
            txt_chat.SelectionColor = Color.Black;
            txt_chat.AppendText($"{nickname}: {message}{Environment.NewLine}");
        }

        public void AppendEmojiMessage(string nickname, string emojiFileName)
        {
            if (txt_chat.InvokeRequired)
            {
                txt_chat.Invoke(new Action(() => AppendEmojiMessage(nickname, emojiFileName)));
                return;
            }

            txt_chat.SelectionStart = txt_chat.TextLength;
            txt_chat.SelectionColor = Color.Black;
            txt_chat.AppendText($"{nickname}: ");

            // 이미지 삽입
            string imgPath = Path.Combine(Application.StartupPath, "img", emojiFileName);
            if (!File.Exists(imgPath))
            {
                imgPath = Path.Combine(Application.StartupPath, @"..\..\img", emojiFileName);
                imgPath = Path.GetFullPath(imgPath);
            }
            
            if (File.Exists(imgPath))
            {
                try
                {
                    using (Image img = Image.FromFile(imgPath))
                    {
                        Clipboard.SetImage(img);
                        txt_chat.ReadOnly = false; // 이미지 삽입 위해 잠깐 해제
                        txt_chat.Paste();
                        txt_chat.ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    txt_chat.AppendText($"[이모지: {emojiFileName}]");
                }
            }
            else
            {
                txt_chat.AppendText($"[이모지: {emojiFileName}]");
            }
            txt_chat.AppendText(Environment.NewLine);
        }
    }
}
