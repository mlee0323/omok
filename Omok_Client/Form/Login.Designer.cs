namespace Omok_Client.Form
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
            this.SuspendLayout();
            // 
            // login_id
            // 
            this.login_id.Location = new System.Drawing.Point(83, 97);
            this.login_id.Margin = new System.Windows.Forms.Padding(1);
            this.login_id.Name = "login_id";
            this.login_id.Size = new System.Drawing.Size(166, 19);
            this.login_id.TabIndex = 0;
            this.login_id.TextChanged += new System.EventHandler(this.login_TextChanged);
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(274, 97);
            this.login_btn.Margin = new System.Windows.Forms.Padding(1);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(111, 47);
            this.login_btn.TabIndex = 2;
            this.login_btn.Text = "로그인";
            this.login_btn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // signup_btn
            // 
            this.signup_btn.Location = new System.Drawing.Point(83, 163);
            this.signup_btn.Margin = new System.Windows.Forms.Padding(1);
            this.signup_btn.Name = "signup_btn";
            this.signup_btn.Size = new System.Drawing.Size(111, 37);
            this.signup_btn.TabIndex = 3;
            this.signup_btn.Text = "회원가입";
            this.signup_btn.Click += new System.EventHandler(this.signup_btn_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.Location = new System.Drawing.Point(274, 163);
            this.exit_btn.Margin = new System.Windows.Forms.Padding(1);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(111, 37);
            this.exit_btn.TabIndex = 4;
            this.exit_btn.Text = "나가기";
            this.exit_btn.Click += new System.EventHandler(this.exit_btn_Click);
            // 
            // login_pw
            // 
            this.login_pw.Location = new System.Drawing.Point(83, 125);
            this.login_pw.Margin = new System.Windows.Forms.Padding(1);
            this.login_pw.Name = "login_pw";
            this.login_pw.Size = new System.Drawing.Size(166, 19);
            this.login_pw.TabIndex = 1;
            this.login_pw.TextChanged += new System.EventHandler(this.login_TextChanged);
            this.login_pw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_pw_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 45);
            this.label1.TabIndex = 7;
            this.label1.Text = "오목게임";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 227);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.login_pw);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.signup_btn);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.login_id);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Login";
            this.Padding = new System.Windows.Forms.Padding(12, 60, 12, 9);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
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
    }
}

