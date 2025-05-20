namespace Omok_Client
{
    partial class Login
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.login_id = new MetroFramework.Controls.MetroTextBox();
            this.login_btn = new MetroFramework.Controls.MetroButton();
            this.signup_btn = new MetroFramework.Controls.MetroButton();
            this.exit_btn = new MetroFramework.Controls.MetroButton();
            this.login_pw = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.login_ip = new MetroFramework.Controls.MetroTextBox();
            this.btn_Server_Open = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // login_id
            // 
            this.login_id.Location = new System.Drawing.Point(118, 133);
            this.login_id.Margin = new System.Windows.Forms.Padding(2);
            this.login_id.Name = "login_id";
            this.login_id.Size = new System.Drawing.Size(236, 29);
            this.login_id.TabIndex = 0;
            this.login_id.TextChanged += new System.EventHandler(this.login_TextChanged);
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(392, 133);
            this.login_btn.Margin = new System.Windows.Forms.Padding(2);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(159, 71);
            this.login_btn.TabIndex = 1;
            this.login_btn.Text = "로그인";
            this.login_btn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // signup_btn
            // 
            this.signup_btn.Location = new System.Drawing.Point(118, 262);
            this.signup_btn.Margin = new System.Windows.Forms.Padding(2);
            this.signup_btn.Name = "signup_btn";
            this.signup_btn.Size = new System.Drawing.Size(159, 56);
            this.signup_btn.TabIndex = 2;
            this.signup_btn.Text = "회원가입";
            this.signup_btn.Click += new System.EventHandler(this.signup_btn_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.Location = new System.Drawing.Point(392, 262);
            this.exit_btn.Margin = new System.Windows.Forms.Padding(2);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(159, 56);
            this.exit_btn.TabIndex = 3;
            this.exit_btn.Text = "나가기";
            this.exit_btn.Click += new System.EventHandler(this.exit_btn_Click);
            // 
            // login_pw
            // 
            this.login_pw.Location = new System.Drawing.Point(118, 175);
            this.login_pw.Margin = new System.Windows.Forms.Padding(2);
            this.login_pw.Name = "login_pw";
            this.login_pw.Size = new System.Drawing.Size(236, 29);
            this.login_pw.TabIndex = 5;
            this.login_pw.TextChanged += new System.EventHandler(this.login_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 65);
            this.label1.TabIndex = 7;
            this.label1.Text = "오목게임";
            // 
            // login_ip
            // 
            this.login_ip.Location = new System.Drawing.Point(118, 219);
            this.login_ip.Margin = new System.Windows.Forms.Padding(2);
            this.login_ip.Name = "login_ip";
            this.login_ip.Size = new System.Drawing.Size(236, 29);
            this.login_ip.TabIndex = 8;
            this.login_ip.TextChanged += new System.EventHandler(this.login_TextChanged);
            // 
            // btn_Server_Open
            // 
            this.btn_Server_Open.Location = new System.Drawing.Point(392, 219);
            this.btn_Server_Open.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Server_Open.Name = "btn_Server_Open";
            this.btn_Server_Open.Size = new System.Drawing.Size(159, 27);
            this.btn_Server_Open.TabIndex = 10;
            this.btn_Server_Open.Text = "서버 열기";
            this.btn_Server_Open.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_Server_Open.Click += new System.EventHandler(this.btn_Server_Open_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 374);
            this.Controls.Add(this.btn_Server_Open);
            this.Controls.Add(this.login_ip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.login_pw);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.signup_btn);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.login_id);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Login";
            this.Padding = new System.Windows.Forms.Padding(17, 60, 17, 14);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox login_id;
        private MetroFramework.Controls.MetroButton login_btn;
        private MetroFramework.Controls.MetroButton signup_btn;
        private MetroFramework.Controls.MetroButton exit_btn;
        private MetroFramework.Controls.MetroTextBox login_pw;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox login_ip;
        private MetroFramework.Controls.MetroButton btn_Server_Open;
    }
}

