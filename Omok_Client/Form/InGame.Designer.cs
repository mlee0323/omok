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
            this.txt_chat = new MetroFramework.Controls.MetroTextBox();
            this.txt_msg = new MetroFramework.Controls.MetroTextBox();
            this.btn_send = new MetroFramework.Controls.MetroButton();
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
            this.btn_skill_1.Location = new System.Drawing.Point(10, 35);
            this.btn_skill_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_skill_1.Name = "btn_skill_1";
            this.btn_skill_1.Size = new System.Drawing.Size(248, 67);
            this.btn_skill_1.TabIndex = 1;
            this.btn_skill_1.Text = "무르기";
            // 
            // btn_skill_2
            // 
            this.btn_skill_2.Location = new System.Drawing.Point(10, 129);
            this.btn_skill_2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_skill_2.Name = "btn_skill_2";
            this.btn_skill_2.Size = new System.Drawing.Size(248, 67);
            this.btn_skill_2.TabIndex = 2;
            this.btn_skill_2.Text = "시간 추가";
            // 
            // btn_skill_3
            // 
            this.btn_skill_3.Location = new System.Drawing.Point(10, 225);
            this.btn_skill_3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_skill_3.Name = "btn_skill_3";
            this.btn_skill_3.Size = new System.Drawing.Size(248, 67);
            this.btn_skill_3.TabIndex = 4;
            this.btn_skill_3.Text = "엎기";
            // 
            // showTurnLabel
            // 
            this.showTurnLabel.AutoSize = true;
            this.showTurnLabel.Location = new System.Drawing.Point(10, 58);
            this.showTurnLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.pn_board.Location = new System.Drawing.Point(22, 217);
            this.pn_board.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pn_board.Name = "pn_board";
            this.pn_board.Size = new System.Drawing.Size(900, 900);
            this.pn_board.TabIndex = 16;
            this.pn_board.VerticalScrollbarBarColor = false;
            this.pn_board.VerticalScrollbarHighlightOnWheel = false;
            this.pn_board.VerticalScrollbarSize = 0;
            // 
            // userLabel2
            // 
            this.userLabel2.AutoSize = true;
            this.userLabel2.Location = new System.Drawing.Point(10, 350);
            this.userLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userLabel2.Name = "userLabel2";
            this.userLabel2.Size = new System.Drawing.Size(42, 19);
            this.userLabel2.TabIndex = 17;
            this.userLabel2.Text = "User2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lv_teamA);
            this.groupBox1.Controls.Add(this.userLabel2);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(408, 135);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "A Team";
            // 
            // lv_teamA
            // 
            this.lv_teamA.HideSelection = false;
            this.lv_teamA.Location = new System.Drawing.Point(12, 34);
            this.lv_teamA.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lv_teamA.Name = "lv_teamA";
            this.lv_teamA.Size = new System.Drawing.Size(384, 89);
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
            this.groupBox3.Location = new System.Drawing.Point(932, 327);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(432, 427);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Team";
            // 
            // btn_ready
            // 
            this.btn_ready.Location = new System.Drawing.Point(234, 331);
            this.btn_ready.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_ready.Name = "btn_ready";
            this.btn_ready.Size = new System.Drawing.Size(186, 83);
            this.btn_ready.TabIndex = 2;
            this.btn_ready.Text = "게임 준비";
            this.btn_ready.Click += new System.EventHandler(this.btn_ready_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_teamB);
            this.groupBox2.Controls.Add(this.metroLabel1);
            this.groupBox2.Location = new System.Drawing.Point(12, 184);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(408, 135);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "B Team";
            // 
            // lv_teamB
            // 
            this.lv_teamB.HideSelection = false;
            this.lv_teamB.Location = new System.Drawing.Point(12, 37);
            this.lv_teamB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lv_teamB.Name = "lv_teamB";
            this.lv_teamB.Size = new System.Drawing.Size(380, 89);
            this.lv_teamB.TabIndex = 19;
            this.lv_teamB.UseCompatibleStateImageBehavior = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(10, 350);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(42, 19);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "User2";
            // 
            // btn_shuffle
            // 
            this.btn_shuffle.Location = new System.Drawing.Point(136, 348);
            this.btn_shuffle.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_shuffle.Name = "btn_shuffle";
            this.btn_shuffle.Size = new System.Drawing.Size(86, 44);
            this.btn_shuffle.TabIndex = 24;
            this.btn_shuffle.Text = "팀 셔플";
            // 
            // btn_move
            // 
            this.btn_move.Location = new System.Drawing.Point(28, 348);
            this.btn_move.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_move.Name = "btn_move";
            this.btn_move.Size = new System.Drawing.Size(86, 44);
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
            this.groupBox4.Location = new System.Drawing.Point(932, 131);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Size = new System.Drawing.Size(442, 185);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Game Info";
            // 
            // lbl_player
            // 
            this.lbl_player.AutoSize = true;
            this.lbl_player.Location = new System.Drawing.Point(200, 133);
            this.lbl_player.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_player.Name = "lbl_player";
            this.lbl_player.Size = new System.Drawing.Size(66, 19);
            this.lbl_player.TabIndex = 17;
            this.lbl_player.Text = "username";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(10, 133);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(104, 19);
            this.metroLabel2.TabIndex = 16;
            this.metroLabel2.Text = "현재 플레이어: ";
            // 
            // lbl_turn
            // 
            this.lbl_turn.AutoSize = true;
            this.lbl_turn.Location = new System.Drawing.Point(200, 58);
            this.lbl_turn.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_turn.Name = "lbl_turn";
            this.lbl_turn.Size = new System.Drawing.Size(16, 19);
            this.lbl_turn.TabIndex = 15;
            this.lbl_turn.Text = "0";
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Location = new System.Drawing.Point(474, 179);
            this.lbl_timer.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(16, 19);
            this.lbl_timer.TabIndex = 19;
            this.lbl_timer.Text = "0";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(362, 179);
            this.metroLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.groupBox5.Location = new System.Drawing.Point(932, 766);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox5.Size = new System.Drawing.Size(268, 302);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Skill";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_music);
            this.groupBox6.Controls.Add(this.btn_chat);
            this.groupBox6.Controls.Add(this.btn_exit);
            this.groupBox6.Location = new System.Drawing.Point(1212, 766);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox6.Size = new System.Drawing.Size(152, 302);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Menu";
            // 
            // btn_music
            // 
            this.btn_music.Location = new System.Drawing.Point(10, 35);
            this.btn_music.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_music.Name = "btn_music";
            this.btn_music.Size = new System.Drawing.Size(136, 67);
            this.btn_music.TabIndex = 2;
            this.btn_music.Text = "음악 끄기";
            // 
            // btn_chat
            // 
            this.btn_chat.Location = new System.Drawing.Point(10, 129);
            this.btn_chat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_chat.Name = "btn_chat";
            this.btn_chat.Size = new System.Drawing.Size(136, 67);
            this.btn_chat.TabIndex = 1;
            this.btn_chat.Text = "채팅 끄기";
            // 
            // txt_chat
            // 
            this.txt_chat.Location = new System.Drawing.Point(1383, 131);
            this.txt_chat.Multiline = true;
            this.txt_chat.Name = "txt_chat";
            this.txt_chat.Size = new System.Drawing.Size(418, 937);
            this.txt_chat.TabIndex = 27;
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(1383, 1081);
            this.txt_msg.Multiline = true;
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(327, 36);
            this.txt_msg.TabIndex = 28;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(1716, 1081);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(85, 36);
            this.btn_send.TabIndex = 29;
            this.btn_send.Text = "전송";
            // 
            // InGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1824, 1354);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.txt_chat);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pn_board);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "InGame";
            this.Padding = new System.Windows.Forms.Padding(20, 125, 20, 21);
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
        private MetroFramework.Controls.MetroTextBox txt_chat;
        private MetroFramework.Controls.MetroTextBox txt_msg;
        private MetroFramework.Controls.MetroButton btn_send;
    }
}