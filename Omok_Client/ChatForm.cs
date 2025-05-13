using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok
{
    public partial class ChatForm : UserControl
    {
        public ChatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {

        }

        private void chatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                // TODO : TextBox 에 내용 입력하고 Enter 눌렀을 때 채팅 전송 구현 
                // ChatListBox.Items.Add 

                chatInput.Text = "";
            }
        }

        private void ChatListBox_LocationChanged(object sender, EventArgs e)
        {
           
        }
    }
}
