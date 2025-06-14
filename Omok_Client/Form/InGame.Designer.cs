namespace Omok_Client.Form
{
    partial class InGame
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
            this.btn_exit = new MetroFramework.Controls.MetroButton();
            this.btn_skill_1 = new MetroFramework.Controls.MetroButton();
            this.btn_skill_2 = new MetroFramework.Controls.MetroButton();
            this.btn_skill_3 = new MetroFramework.Controls.MetroButton();
            this.showTurnLabel = new MetroFramework.Controls.MetroLabel();
            this.pn_board = new MetroFramework.Controls.MetroPanel();
            this.userLabel2 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lv_teamA = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_ready = new MetroFramework.Controls.MetroButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lv_teamB = new System.Windows.Forms.ListView();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btn_shuffle = new MetroFramework.Controls.MetroButton();
            this.btn_move = new MetroFramework.Controls.MetroButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbl_player = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lbl_turn = new MetroFramework.Controls.MetroLabel();
            this.lbl_timer = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_music = new MetroFramework.Controls.MetroButton();
            this.btn_chat = new MetroFramework.Controls.MetroButton();
            this.MySelfLabel = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(10, 225);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(134, 67);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.Text = "나가기";
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_skill_1
            // 
            this.btn_skill_1.Location = new System.Drawing.Point(8, 25);
            this.btn_skill_1.Name = "btn_skill_1";
            this.btn_skill_1.Size = new System.Drawing.Size(207, 48);
            this.btn_skill_1.TabIndex = 1;
            this.btn_skill_1.Text = "무르기";
            // 
            // btn_skill_2
            // 
            this.btn_skill_2.Location = new System.Drawing.Point(8, 93);
            this.btn_skill_2.Name = "btn_skill_2";
            this.btn_skill_2.Size = new System.Drawing.Size(207, 48);
            this.btn_skill_2.TabIndex = 2;
            this.btn_skill_2.Text = "시간 추가";
            // 
            // btn_skill_3
            // 
            this.btn_skill_3.Location = new System.Drawing.Point(8, 162);
            this.btn_skill_3.Name = "btn_skill_3";
            this.btn_skill_3.Size = new System.Drawing.Size(207, 48);
            this.btn_skill_3.TabIndex = 4;
            this.btn_skill_3.Text = "엎기";
            // 
            // showTurnLabel
            // 
            this.showTurnLabel.AutoSize = true;
            this.showTurnLabel.Location = new System.Drawing.Point(8, 42);
            this.showTurnLabel.Name = "showTurnLabel";
            this.showTurnLabel.Size = new System.Drawing.Size(76, 19);
            this.showTurnLabel.TabIndex = 14;
            this.showTurnLabel.Text = "현재 착수: ";
            // 
            // pn_board
            // 
            this.pn_board.HorizontalScrollbarBarColor = false;
            this.pn_board.HorizontalScrollbarHighlightOnWheel = false;
            this.pn_board.HorizontalScrollbarSize = 0;
            this.pn_board.Location = new System.Drawing.Point(18, 156);
            this.pn_board.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.pn_board.Name = "pn_board";
            this.pn_board.Size = new System.Drawing.Size(750, 648);
            this.pn_board.TabIndex = 16;
            this.pn_board.VerticalScrollbarBarColor = false;
            this.pn_board.VerticalScrollbarHighlightOnWheel = false;
            this.pn_board.VerticalScrollbarSize = 0;
            // 
            // userLabel2
            // 
            this.userLabel2.AutoSize = true;
            this.userLabel2.Location = new System.Drawing.Point(8, 252);
            this.userLabel2.Name = "userLabel2";
            this.userLabel2.Size = new System.Drawing.Size(42, 19);
            this.userLabel2.TabIndex = 17;
            this.userLabel2.Text = "User2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lv_teamA);
            this.groupBox1.Controls.Add(this.userLabel2);
            this.groupBox1.Location = new System.Drawing.Point(10, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(340, 97);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "A Team";
            // 
            // lv_teamA
            // 
            this.lv_teamA.HideSelection = false;
            this.lv_teamA.Location = new System.Drawing.Point(10, 24);
            this.lv_teamA.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lv_teamA.Name = "lv_teamA";
            this.lv_teamA.Size = new System.Drawing.Size(321, 65);
            this.lv_teamA.TabIndex = 18;
            this.lv_teamA.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_ready);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.btn_shuffle);
            this.groupBox3.Controls.Add(this.btn_move);
            this.groupBox3.Location = new System.Drawing.Point(777, 235);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Size = new System.Drawing.Size(360, 307);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Team";
            // 
            // btn_ready
            // 
            this.btn_ready.Location = new System.Drawing.Point(195, 238);
            this.btn_ready.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_ready.Name = "btn_ready";
            this.btn_ready.Size = new System.Drawing.Size(155, 60);
            this.btn_ready.TabIndex = 2;
            this.btn_ready.Text = "게임 준비";
            this.btn_ready.Click += new System.EventHandler(this.btn_ready_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_teamB);
            this.groupBox2.Controls.Add(this.metroLabel1);
            this.groupBox2.Location = new System.Drawing.Point(10, 132);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Size = new System.Drawing.Size(340, 97);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "B Team";
            // 
            // lv_teamB
            // 
            this.lv_teamB.HideSelection = false;
            this.lv_teamB.Location = new System.Drawing.Point(10, 27);
            this.lv_teamB.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lv_teamB.Name = "lv_teamB";
            this.lv_teamB.Size = new System.Drawing.Size(317, 65);
            this.lv_teamB.TabIndex = 19;
            this.lv_teamB.UseCompatibleStateImageBehavior = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(8, 252);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(42, 19);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "User2";
            // 
            // btn_shuffle
            // 
            this.btn_shuffle.Location = new System.Drawing.Point(113, 251);
            this.btn_shuffle.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_shuffle.Name = "btn_shuffle";
            this.btn_shuffle.Size = new System.Drawing.Size(72, 32);
            this.btn_shuffle.TabIndex = 24;
            this.btn_shuffle.Text = "팀 셔플";
            // 
            // btn_move
            // 
            this.btn_move.Location = new System.Drawing.Point(23, 251);
            this.btn_move.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_move.Name = "btn_move";
            this.btn_move.Size = new System.Drawing.Size(72, 32);
            this.btn_move.TabIndex = 23;
            this.btn_move.Text = "팀 이동";
            this.btn_move.Click += new System.EventHandler(this.btn_move_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbl_player);
            this.groupBox4.Controls.Add(this.metroLabel2);
            this.groupBox4.Controls.Add(this.lbl_turn);
            this.groupBox4.Controls.Add(this.showTurnLabel);
            this.groupBox4.Location = new System.Drawing.Point(777, 94);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Size = new System.Drawing.Size(368, 133);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Game Info";
            // 
            // lbl_player
            // 
            this.lbl_player.AutoSize = true;
            this.lbl_player.Location = new System.Drawing.Point(167, 96);
            this.lbl_player.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_player.Name = "lbl_player";
            this.lbl_player.Size = new System.Drawing.Size(66, 19);
            this.lbl_player.TabIndex = 17;
            this.lbl_player.Text = "username";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(8, 96);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(104, 19);
            this.metroLabel2.TabIndex = 16;
            this.metroLabel2.Text = "현재 플레이어: ";
            // 
            // lbl_turn
            // 
            this.lbl_turn.AutoSize = true;
            this.lbl_turn.Location = new System.Drawing.Point(167, 42);
            this.lbl_turn.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_turn.Name = "lbl_turn";
            this.lbl_turn.Size = new System.Drawing.Size(16, 19);
            this.lbl_turn.TabIndex = 15;
            this.lbl_turn.Text = "0";
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Location = new System.Drawing.Point(395, 129);
            this.lbl_timer.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(16, 19);
            this.lbl_timer.TabIndex = 19;
            this.lbl_timer.Text = "0";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(302, 129);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(51, 19);
            this.metroLabel3.TabIndex = 18;
            this.metroLabel3.Text = "타이머";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_skill_1);
            this.groupBox5.Controls.Add(this.btn_skill_2);
            this.groupBox5.Controls.Add(this.btn_skill_3);
            this.groupBox5.Location = new System.Drawing.Point(777, 552);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Size = new System.Drawing.Size(223, 217);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Skill";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_music);
            this.groupBox6.Controls.Add(this.btn_chat);
            this.groupBox6.Controls.Add(this.btn_exit);
            this.groupBox6.Location = new System.Drawing.Point(1010, 552);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox6.Size = new System.Drawing.Size(127, 217);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Menu";
            // 
            // btn_music
            // 
            this.btn_music.Location = new System.Drawing.Point(8, 25);
            this.btn_music.Name = "btn_music";
            this.btn_music.Size = new System.Drawing.Size(113, 48);
            this.btn_music.TabIndex = 2;
            this.btn_music.Text = "음악 끄기";
            // 
            // btn_chat
            // 
            this.btn_chat.Location = new System.Drawing.Point(8, 93);
            this.btn_chat.Name = "btn_chat";
            this.btn_chat.Size = new System.Drawing.Size(113, 48);
            this.btn_chat.TabIndex = 1;
            this.btn_chat.Text = "채팅 끄기";
            // 
            this.MySelfLabel.AutoSize = true;
            this.MySelfLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.MySelfLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.MySelfLabel.Name = "MySelfLabel";
            this.MySelfLabel.Location = new System.Drawing.Point(488, 19);
            this.MySelfLabel.Size = new System.Drawing.Size(0, 0);
            this.MySelfLabel.TabIndex = 30;
            // 
            // MySelfLabel
            // 
            // InGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1671, 975);
            this.Controls.Add(this.MySelfLabel);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pn_board);
            this.Name = "InGame";
            this.Padding = new System.Windows.Forms.Padding(17, 90, 17, 15);
            this.Text = "Game Code: ";
            this.Load += new System.EventHandler(this.InGame_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_exit;
        private MetroFramework.Controls.MetroButton btn_skill_1;
        private MetroFramework.Controls.MetroButton btn_skill_2;
        private MetroFramework.Controls.MetroButton btn_skill_3;
        private MetroFramework.Controls.MetroLabel showTurnLabel;
        private MetroFramework.Controls.MetroPanel pn_board;
        private MetroFramework.Controls.MetroLabel userLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton btn_move;
        private System.Windows.Forms.GroupBox groupBox4;
        private MetroFramework.Controls.MetroLabel lbl_turn;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lbl_player;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private MetroFramework.Controls.MetroButton btn_shuffle;
        private MetroFramework.Controls.MetroButton btn_music;
        private MetroFramework.Controls.MetroButton btn_chat;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel lbl_timer;
        private MetroFramework.Controls.MetroButton btn_ready;
        private System.Windows.Forms.ListView lv_teamA;
        private System.Windows.Forms.ListView lv_teamB;
        private MetroFramework.Controls.MetroLabel MySelfLabel;
    }
}