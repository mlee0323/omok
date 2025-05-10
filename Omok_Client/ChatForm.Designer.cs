namespace Omok
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatInput = new MetroFramework.Controls.MetroTextBox();
            this.ChatListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // chatInput
            // 
            this.chatInput.BackColor = System.Drawing.Color.White;
            this.chatInput.Location = new System.Drawing.Point(12, 683);
            this.chatInput.Name = "chatInput";
            this.chatInput.Size = new System.Drawing.Size(258, 70);
            this.chatInput.TabIndex = 1;
            this.chatInput.Text = "채팅 입력창 ( 임시 )";
            this.chatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatInput_KeyDown);
            // 
            // ChatListBox
            // 
            this.ChatListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ChatListBox.FormattingEnabled = true;
            this.ChatListBox.ItemHeight = 15;
            this.ChatListBox.Location = new System.Drawing.Point(12, 12);
            this.ChatListBox.Name = "ChatListBox";
            this.ChatListBox.Size = new System.Drawing.Size(258, 664);
            this.ChatListBox.TabIndex = 2;
            this.ChatListBox.LocationChanged += new System.EventHandler(this.ChatListBox_LocationChanged);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(282, 765);
            this.Controls.Add(this.ChatListBox);
            this.Controls.Add(this.chatInput);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox chatInput;
        private System.Windows.Forms.ListBox ChatListBox;
    }
}