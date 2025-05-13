namespace Omok
{
    partial class GameMainScreen
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
            this.ExitButton = new MetroFramework.Controls.MetroButton();
            this.SkillButton1 = new MetroFramework.Controls.MetroButton();
            this.SkillButton2 = new MetroFramework.Controls.MetroButton();
            this.ShuffleButton = new MetroFramework.Controls.MetroButton();
            this.SkillButton3 = new MetroFramework.Controls.MetroButton();
            this.timeOutButton = new MetroFramework.Controls.MetroButton();
            this.userLabel1 = new MetroFramework.Controls.MetroLabel();
            this.userLabel2 = new MetroFramework.Controls.MetroLabel();
            this.userLabel3 = new MetroFramework.Controls.MetroLabel();
            this.userLabel4 = new MetroFramework.Controls.MetroLabel();
            this.MainPanel = new MetroFramework.Controls.MetroPanel();
            this.ShowTurnLabel = new MetroFramework.Controls.MetroLabel();
            this.CurrentTurnLabel = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(432, 713);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(180, 40);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "나가기";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // SkillButton1
            // 
            this.SkillButton1.Location = new System.Drawing.Point(61, 664);
            this.SkillButton1.Name = "SkillButton1";
            this.SkillButton1.Size = new System.Drawing.Size(150, 40);
            this.SkillButton1.TabIndex = 1;
            this.SkillButton1.Text = "스킬 1";
            this.SkillButton1.Click += new System.EventHandler(this.SkillButton1_Click);
            // 
            // SkillButton2
            // 
            this.SkillButton2.Location = new System.Drawing.Point(237, 664);
            this.SkillButton2.Name = "SkillButton2";
            this.SkillButton2.Size = new System.Drawing.Size(150, 40);
            this.SkillButton2.TabIndex = 2;
            this.SkillButton2.Text = "스킬 2";
            this.SkillButton2.Click += new System.EventHandler(this.SkillButton2_Click);
            // 
            // ShuffleButton
            // 
            this.ShuffleButton.Location = new System.Drawing.Point(222, 713);
            this.ShuffleButton.Name = "ShuffleButton";
            this.ShuffleButton.Size = new System.Drawing.Size(180, 40);
            this.ShuffleButton.TabIndex = 3;
            this.ShuffleButton.Text = "팀 셔플";
            this.ShuffleButton.Click += new System.EventHandler(this.ShuffleButton_Click);
            // 
            // SkillButton3
            // 
            this.SkillButton3.Location = new System.Drawing.Point(410, 664);
            this.SkillButton3.Name = "SkillButton3";
            this.SkillButton3.Size = new System.Drawing.Size(150, 40);
            this.SkillButton3.TabIndex = 4;
            this.SkillButton3.Text = "스킬 3";
            this.SkillButton3.Click += new System.EventHandler(this.SkillButton3_Click);
            // 
            // timeOutButton
            // 
            this.timeOutButton.Location = new System.Drawing.Point(12, 713);
            this.timeOutButton.Name = "timeOutButton";
            this.timeOutButton.Size = new System.Drawing.Size(180, 40);
            this.timeOutButton.TabIndex = 5;
            this.timeOutButton.Text = "타임아웃";
            this.timeOutButton.Click += new System.EventHandler(this.timeOutButton_Click);
            // 
            // userLabel1
            // 
            this.userLabel1.AutoSize = true;
            this.userLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.userLabel1.Location = new System.Drawing.Point(9, 9);
            this.userLabel1.Name = "userLabel1";
            this.userLabel1.Size = new System.Drawing.Size(43, 20);
            this.userLabel1.TabIndex = 6;
            this.userLabel1.Text = "User1";
            // 
            // userLabel2
            // 
            this.userLabel2.AutoSize = true;
            this.userLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.userLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.userLabel2.Location = new System.Drawing.Point(566, 9);
            this.userLabel2.Name = "userLabel2";
            this.userLabel2.Size = new System.Drawing.Size(46, 20);
            this.userLabel2.TabIndex = 7;
            this.userLabel2.Text = "User2";
            // 
            // userLabel3
            // 
            this.userLabel3.AutoSize = true;
            this.userLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.userLabel3.Location = new System.Drawing.Point(9, 664);
            this.userLabel3.Name = "userLabel3";
            this.userLabel3.Size = new System.Drawing.Size(46, 20);
            this.userLabel3.TabIndex = 8;
            this.userLabel3.Text = "User3";
            // 
            // userLabel4
            // 
            this.userLabel4.AutoSize = true;
            this.userLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.userLabel4.Location = new System.Drawing.Point(566, 664);
            this.userLabel4.Name = "userLabel4";
            this.userLabel4.Size = new System.Drawing.Size(46, 20);
            this.userLabel4.TabIndex = 9;
            this.userLabel4.Text = "User4";
            // 
            // MainPanel
            // 
            this.MainPanel.HorizontalScrollbarBarColor = true;
            this.MainPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.MainPanel.HorizontalScrollbarSize = 10;
            this.MainPanel.Location = new System.Drawing.Point(12, 45);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(600, 600);
            this.MainPanel.TabIndex = 10;
            this.MainPanel.VerticalScrollbarBarColor = true;
            this.MainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.MainPanel.VerticalScrollbarSize = 10;
            this.MainPanel.Click += new System.EventHandler(this.MainPanel_Click);
            // 
            // ShowTurnLabel
            // 
            this.ShowTurnLabel.AutoSize = true;
            this.ShowTurnLabel.Location = new System.Drawing.Point(222, 9);
            this.ShowTurnLabel.Name = "ShowTurnLabel";
            this.ShowTurnLabel.Size = new System.Drawing.Size(65, 20);
            this.ShowTurnLabel.TabIndex = 11;
            this.ShowTurnLabel.Text = "현재 턴 :";
            // 
            // CurrentTurnLabel
            // 
            this.CurrentTurnLabel.AutoSize = true;
            this.CurrentTurnLabel.Location = new System.Drawing.Point(303, 9);
            this.CurrentTurnLabel.Name = "CurrentTurnLabel";
            this.CurrentTurnLabel.Size = new System.Drawing.Size(107, 20);
            this.CurrentTurnLabel.TabIndex = 12;
            this.CurrentTurnLabel.Text = "현재 턴 사용자";
            // 
            // GameMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(621, 765);
            this.Controls.Add(this.CurrentTurnLabel);
            this.Controls.Add(this.ShowTurnLabel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.userLabel4);
            this.Controls.Add(this.userLabel3);
            this.Controls.Add(this.userLabel2);
            this.Controls.Add(this.userLabel1);
            this.Controls.Add(this.timeOutButton);
            this.Controls.Add(this.SkillButton3);
            this.Controls.Add(this.ShuffleButton);
            this.Controls.Add(this.SkillButton2);
            this.Controls.Add(this.SkillButton1);
            this.Controls.Add(this.ExitButton);
            this.Name = "GameMainScreen";
            this.Text = "GameMainScreen";
            this.Load += new System.EventHandler(this.GameMainScreen_Load);
            this.LocationChanged += new System.EventHandler(this.GameMainScreen_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton ExitButton;
        private MetroFramework.Controls.MetroButton SkillButton1;
        private MetroFramework.Controls.MetroButton SkillButton2;
        private MetroFramework.Controls.MetroButton ShuffleButton;
        private MetroFramework.Controls.MetroButton SkillButton3;
        private MetroFramework.Controls.MetroButton timeOutButton;
        private MetroFramework.Controls.MetroLabel userLabel1;
        private MetroFramework.Controls.MetroLabel userLabel2;
        private MetroFramework.Controls.MetroLabel userLabel3;
        private MetroFramework.Controls.MetroLabel userLabel4;
        private MetroFramework.Controls.MetroPanel MainPanel;
        private MetroFramework.Controls.MetroLabel ShowTurnLabel;
        private MetroFramework.Controls.MetroLabel CurrentTurnLabel;
    }
}