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
            this.chatInput.Location = new System.Drawing.Point(18, 1045);
            this.chatInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chatInput.Name = "chatInput";
            this.chatInput.Size = new System.Drawing.Size(387, 117);
            this.chatInput.TabIndex = 1;
            this.chatInput.Text = "채팅 입력창 ( 임시 )";
            this.chatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatInput_KeyDown);
            // 
            // ChatListBox
            // 
            this.ChatListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ChatListBox.FormattingEnabled = true;
            this.ChatListBox.ItemHeight = 25;
            this.ChatListBox.Location = new System.Drawing.Point(20, 0);
            this.ChatListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChatListBox.Name = "ChatListBox";
            this.ChatListBox.Size = new System.Drawing.Size(385, 1004);
            this.ChatListBox.TabIndex = 2;
            this.ChatListBox.LocationChanged += new System.EventHandler(this.ChatListBox_LocationChanged);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ChatListBox);
            this.Controls.Add(this.chatInput);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChatForm";
            this.Size = new System.Drawing.Size(423, 1275);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox chatInput;
        private System.Windows.Forms.ListBox ChatListBox;
    }
}