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
            this.showTurnLabel = new MetroFramework.Controls.MetroLabel();
            this.CurrentTurnLabel = new MetroFramework.Controls.MetroLabel();
            this.MainPanel = new MetroFramework.Controls.MetroPanel();
            this.userLabel2 = new MetroFramework.Controls.MetroLabel();
            this.userLabel3 = new MetroFramework.Controls.MetroLabel();
            this.userLabel4 = new MetroFramework.Controls.MetroLabel();
            this.ChattingPanel = new MetroFramework.Controls.MetroPanel();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(647, 1228);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(270, 67);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "나가기";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // SkillButton1
            // 
            this.SkillButton1.Location = new System.Drawing.Point(62, 1131);
            this.SkillButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SkillButton1.Name = "SkillButton1";
            this.SkillButton1.Size = new System.Drawing.Size(225, 67);
            this.SkillButton1.TabIndex = 1;
            this.SkillButton1.Text = "스킬 1";
            this.SkillButton1.Click += new System.EventHandler(this.SkillButton1_Click);
            // 
            // SkillButton2
            // 
            this.SkillButton2.Location = new System.Drawing.Point(363, 1131);
            this.SkillButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SkillButton2.Name = "SkillButton2";
            this.SkillButton2.Size = new System.Drawing.Size(225, 67);
            this.SkillButton2.TabIndex = 2;
            this.SkillButton2.Text = "스킬 2";
            this.SkillButton2.Click += new System.EventHandler(this.SkillButton2_Click);
            // 
            // ShuffleButton
            // 
            this.ShuffleButton.Location = new System.Drawing.Point(340, 1228);
            this.ShuffleButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ShuffleButton.Name = "ShuffleButton";
            this.ShuffleButton.Size = new System.Drawing.Size(270, 67);
            this.ShuffleButton.TabIndex = 3;
            this.ShuffleButton.Text = "팀 셔플";
            this.ShuffleButton.Click += new System.EventHandler(this.ShuffleButton_Click);
            // 
            // SkillButton3
            // 
            this.SkillButton3.Location = new System.Drawing.Point(647, 1131);
            this.SkillButton3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SkillButton3.Name = "SkillButton3";
            this.SkillButton3.Size = new System.Drawing.Size(225, 67);
            this.SkillButton3.TabIndex = 4;
            this.SkillButton3.Text = "스킬 3";
            this.SkillButton3.Click += new System.EventHandler(this.SkillButton3_Click);
            // 
            // timeOutButton
            // 
            this.timeOutButton.Location = new System.Drawing.Point(17, 1228);
            this.timeOutButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeOutButton.Name = "timeOutButton";
            this.timeOutButton.Size = new System.Drawing.Size(270, 67);
            this.timeOutButton.TabIndex = 5;
            this.timeOutButton.Text = "타임아웃";
            this.timeOutButton.Click += new System.EventHandler(this.timeOutButton_Click);
            // 
            // userLabel1
            // 
            this.userLabel1.AutoSize = true;
            this.userLabel1.Location = new System.Drawing.Point(14, 94);
            this.userLabel1.Name = "userLabel1";
            this.userLabel1.Size = new System.Drawing.Size(40, 19);
            this.userLabel1.TabIndex = 13;
            this.userLabel1.Text = "User1";
            // 
            // showTurnLabel
            // 
            this.showTurnLabel.AutoSize = true;
            this.showTurnLabel.Location = new System.Drawing.Point(282, 94);
            this.showTurnLabel.Name = "showTurnLabel";
            this.showTurnLabel.Size = new System.Drawing.Size(55, 19);
            this.showTurnLabel.TabIndex = 14;
            this.showTurnLabel.Text = "현재 턴";
            // 
            // CurrentTurnLabel
            // 
            this.CurrentTurnLabel.AutoSize = true;
            this.CurrentTurnLabel.Location = new System.Drawing.Point(520, 94);
            this.CurrentTurnLabel.Name = "CurrentTurnLabel";
            this.CurrentTurnLabel.Size = new System.Drawing.Size(101, 19);
            this.CurrentTurnLabel.TabIndex = 15;
            this.CurrentTurnLabel.Text = "현재 턴 사용자";
            // 
            // MainPanel
            // 
            this.MainPanel.HorizontalScrollbarBarColor = false;
            this.MainPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.MainPanel.HorizontalScrollbarSize = 0;
            this.MainPanel.Location = new System.Drawing.Point(17, 134);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(900, 900);
            this.MainPanel.TabIndex = 16;
            this.MainPanel.VerticalScrollbarBarColor = false;
            this.MainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.MainPanel.VerticalScrollbarSize = 0;
            // 
            // userLabel2
            // 
            this.userLabel2.AutoSize = true;
            this.userLabel2.Location = new System.Drawing.Point(831, 94);
            this.userLabel2.Name = "userLabel2";
            this.userLabel2.Size = new System.Drawing.Size(42, 19);
            this.userLabel2.TabIndex = 17;
            this.userLabel2.Text = "User2";
            // 
            // userLabel3
            // 
            this.userLabel3.AutoSize = true;
            this.userLabel3.Location = new System.Drawing.Point(14, 1050);
            this.userLabel3.Name = "userLabel3";
            this.userLabel3.Size = new System.Drawing.Size(42, 19);
            this.userLabel3.TabIndex = 18;
            this.userLabel3.Text = "User3";
            // 
            // userLabel4
            // 
            this.userLabel4.AutoSize = true;
            this.userLabel4.Location = new System.Drawing.Point(831, 1050);
            this.userLabel4.Name = "userLabel4";
            this.userLabel4.Size = new System.Drawing.Size(42, 19);
            this.userLabel4.TabIndex = 19;
            this.userLabel4.Text = "User4";
            // 
            // ChattingPanel
            // 
            this.ChattingPanel.HorizontalScrollbarBarColor = true;
            this.ChattingPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.ChattingPanel.HorizontalScrollbarSize = 0;
            this.ChattingPanel.Location = new System.Drawing.Point(982, 134);
            this.ChattingPanel.Name = "ChattingPanel";
            this.ChattingPanel.Size = new System.Drawing.Size(422, 1163);
            this.ChattingPanel.TabIndex = 20;
            this.ChattingPanel.VerticalScrollbarBarColor = true;
            this.ChattingPanel.VerticalScrollbarHighlightOnWheel = false;
            this.ChattingPanel.VerticalScrollbarSize = 0;
            // 
            // GameMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1462, 1320);
            this.Controls.Add(this.ChattingPanel);
            this.Controls.Add(this.userLabel4);
            this.Controls.Add(this.userLabel3);
            this.Controls.Add(this.userLabel2);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.CurrentTurnLabel);
            this.Controls.Add(this.showTurnLabel);
            this.Controls.Add(this.userLabel1);
            this.Controls.Add(this.timeOutButton);
            this.Controls.Add(this.SkillButton3);
            this.Controls.Add(this.ShuffleButton);
            this.Controls.Add(this.SkillButton2);
            this.Controls.Add(this.SkillButton1);
            this.Controls.Add(this.ExitButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GameMainScreen";
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
        private MetroFramework.Controls.MetroLabel showTurnLabel;
        private MetroFramework.Controls.MetroLabel CurrentTurnLabel;
        private MetroFramework.Controls.MetroPanel MainPanel;
        private MetroFramework.Controls.MetroLabel userLabel2;
        private MetroFramework.Controls.MetroLabel userLabel3;
        private MetroFramework.Controls.MetroLabel userLabel4;
        private MetroFramework.Controls.MetroPanel ChattingPanel;
    }
}