namespace Omok_Client.Control
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_chat = new MetroFramework.Controls.MetroTextBox();
            this.txt_msg = new MetroFramework.Controls.MetroTextBox();
            this.btn_send = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // txt_chat
            // 
            this.txt_chat.Location = new System.Drawing.Point(0, 4);
            this.txt_chat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_chat.Multiline = true;
            this.txt_chat.Name = "txt_chat";
            this.txt_chat.ReadOnly = true;
            this.txt_chat.Size = new System.Drawing.Size(425, 784);
            this.txt_chat.TabIndex = 0;
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(0, 792);
            this.txt_msg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_msg.Multiline = true;
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(315, 45);
            this.txt_msg.TabIndex = 1;
            this.txt_msg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_msg_KeyPress);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(323, 792);
            this.btn_send.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(100, 45);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "전송";
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.txt_chat);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChatForm";
            this.Size = new System.Drawing.Size(429, 842);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txt_chat;
        private MetroFramework.Controls.MetroTextBox txt_msg;
        private MetroFramework.Controls.MetroButton btn_send;
    }
}