namespace Omok_Client.Form
{
    partial class Lobby
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
            this.btn_logout = new MetroFramework.Controls.MetroButton();
            this.btn_exit = new MetroFramework.Controls.MetroButton();
            this.btn_re = new MetroFramework.Controls.MetroButton();
            this.btn_ec = new MetroFramework.Controls.MetroButton();
            this.btn_history = new MetroFramework.Controls.MetroButton();
            this.btn_cr = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.lbl_lose = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.lbl_draw = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lbl_win = new MetroFramework.Controls.MetroLabel();
            this.lbl_username = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_logout
            // 
            this.btn_logout.Location = new System.Drawing.Point(252, 248);
            this.btn_logout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(181, 38);
            this.btn_logout.TabIndex = 0;
            this.btn_logout.Text = "로그아웃";
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(252, 292);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(181, 38);
            this.btn_exit.TabIndex = 1;
            this.btn_exit.Text = "게임 종료";
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_re
            // 
            this.btn_re.Location = new System.Drawing.Point(7, 61);
            this.btn_re.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_re.Name = "btn_re";
            this.btn_re.Size = new System.Drawing.Size(181, 38);
            this.btn_re.TabIndex = 2;
            this.btn_re.Text = "랜덤 참가";
            this.btn_re.Click += new System.EventHandler(this.btn_re_Click);
            // 
            // btn_ec
            // 
            this.btn_ec.Location = new System.Drawing.Point(7, 104);
            this.btn_ec.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_ec.Name = "btn_ec";
            this.btn_ec.Size = new System.Drawing.Size(181, 38);
            this.btn_ec.TabIndex = 3;
            this.btn_ec.Text = "코드로 참여";
            this.btn_ec.Click += new System.EventHandler(this.btn_ec_Click);
            // 
            // btn_history
            // 
            this.btn_history.Location = new System.Drawing.Point(7, 81);
            this.btn_history.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_history.Name = "btn_history";
            this.btn_history.Size = new System.Drawing.Size(181, 38);
            this.btn_history.TabIndex = 5;
            this.btn_history.Text = "게임 기록";
            this.btn_history.Click += new System.EventHandler(this.btn_history_Click);
            // 
            // btn_cr
            // 
            this.btn_cr.Location = new System.Drawing.Point(7, 18);
            this.btn_cr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_cr.Name = "btn_cr";
            this.btn_cr.Size = new System.Drawing.Size(181, 38);
            this.btn_cr.TabIndex = 6;
            this.btn_cr.Text = "방 만들기";
            this.btn_cr.Click += new System.EventHandler(this.btn_cr_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(28, 59);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(79, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "안녕하세요";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_cr);
            this.groupBox1.Controls.Add(this.btn_re);
            this.groupBox1.Controls.Add(this.btn_ec);
            this.groupBox1.Location = new System.Drawing.Point(27, 93);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(198, 150);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "플레이";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.metroLabel5);
            this.groupBox2.Controls.Add(this.lbl_lose);
            this.groupBox2.Controls.Add(this.metroLabel3);
            this.groupBox2.Controls.Add(this.lbl_draw);
            this.groupBox2.Controls.Add(this.metroLabel2);
            this.groupBox2.Controls.Add(this.lbl_win);
            this.groupBox2.Controls.Add(this.btn_history);
            this.groupBox2.Location = new System.Drawing.Point(245, 93);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(198, 126);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "기록";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(134, 37);
            this.metroLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(26, 19);
            this.metroLabel5.TabIndex = 11;
            this.metroLabel5.Text = "패:";
            // 
            // lbl_lose
            // 
            this.lbl_lose.AutoSize = true;
            this.lbl_lose.Location = new System.Drawing.Point(168, 37);
            this.lbl_lose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_lose.Name = "lbl_lose";
            this.lbl_lose.Size = new System.Drawing.Size(16, 19);
            this.lbl_lose.TabIndex = 10;
            this.lbl_lose.Text = "0";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(76, 37);
            this.metroLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(26, 19);
            this.metroLabel3.TabIndex = 9;
            this.metroLabel3.Text = "무:";
            // 
            // lbl_draw
            // 
            this.lbl_draw.AutoSize = true;
            this.lbl_draw.Location = new System.Drawing.Point(110, 37);
            this.lbl_draw.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_draw.Name = "lbl_draw";
            this.lbl_draw.Size = new System.Drawing.Size(16, 19);
            this.lbl_draw.TabIndex = 8;
            this.lbl_draw.Text = "0";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(15, 37);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(26, 19);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "승:";
            // 
            // lbl_win
            // 
            this.lbl_win.AutoSize = true;
            this.lbl_win.Location = new System.Drawing.Point(52, 37);
            this.lbl_win.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_win.Name = "lbl_win";
            this.lbl_win.Size = new System.Drawing.Size(16, 19);
            this.lbl_win.TabIndex = 6;
            this.lbl_win.Text = "0";
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(122, 59);
            this.lbl_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(71, 19);
            this.lbl_username.TabIndex = 10;
            this.lbl_username.Text = "UserName";
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 340);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_logout);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Lobby";
            this.Padding = new System.Windows.Forms.Padding(23, 55, 23, 18);
            this.Text = "로비";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Lobby_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_logout;
        private MetroFramework.Controls.MetroButton btn_exit;
        private MetroFramework.Controls.MetroButton btn_re;
        private MetroFramework.Controls.MetroButton btn_ec;
        private MetroFramework.Controls.MetroButton btn_history;
        private MetroFramework.Controls.MetroButton btn_cr;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroLabel lbl_win;
        private MetroFramework.Controls.MetroLabel lbl_username;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel lbl_lose;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel lbl_draw;
    }
}