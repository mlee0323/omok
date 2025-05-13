namespace Omok
{
    partial class SignUpForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.signup_name = new MetroFramework.Controls.MetroTextBox();
            this.signup_pw2 = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.signup_id = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.id_dupl_chk_btn = new MetroFramework.Controls.MetroButton();
            this.signup_pw = new MetroFramework.Controls.MetroTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.signup_btn = new MetroFramework.Controls.MetroButton();
            this.nn_dupl_chk_btn = new MetroFramework.Controls.MetroButton();
            this.close_btn = new MetroFramework.Controls.MetroButton();
            this.signup_pw2_warning_lb = new System.Windows.Forms.Label();
            this.signup_pw_warning_lb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(23, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "닉네임";
            // 
            // signup_name
            // 
            this.signup_name.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.signup_name.Location = new System.Drawing.Point(23, 114);
            this.signup_name.Name = "signup_name";
            this.signup_name.Size = new System.Drawing.Size(300, 40);
            this.signup_name.TabIndex = 1;
            this.signup_name.TextChanged += new System.EventHandler(this.signup_name_TextChanged);
            // 
            // signup_pw2
            // 
            this.signup_pw2.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.signup_pw2.Location = new System.Drawing.Point(23, 451);
            this.signup_pw2.Name = "signup_pw2";
            this.signup_pw2.Size = new System.Drawing.Size(300, 40);
            this.signup_pw2.TabIndex = 6;
            this.signup_pw2.TextChanged += new System.EventHandler(this.signup_pw2_TextChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(23, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "아이디";
            // 
            // signup_id
            // 
            this.signup_id.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.signup_id.Location = new System.Drawing.Point(23, 214);
            this.signup_id.Name = "signup_id";
            this.signup_id.Size = new System.Drawing.Size(300, 40);
            this.signup_id.TabIndex = 3;
            this.signup_id.TextChanged += new System.EventHandler(this.signup_id_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(16, 282);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 37);
            this.label3.TabIndex = 4;
            this.label3.Text = "패스워드";
            // 
            // id_dupl_chk_btn
            // 
            this.id_dupl_chk_btn.Location = new System.Drawing.Point(356, 214);
            this.id_dupl_chk_btn.Name = "id_dupl_chk_btn";
            this.id_dupl_chk_btn.Size = new System.Drawing.Size(102, 40);
            this.id_dupl_chk_btn.TabIndex = 4;
            this.id_dupl_chk_btn.Text = "중복 확인";
            this.id_dupl_chk_btn.Click += new System.EventHandler(this.id_dupl_chk_btn_Click);
            // 
            // signup_pw
            // 
            this.signup_pw.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.signup_pw.Location = new System.Drawing.Point(23, 322);
            this.signup_pw.Name = "signup_pw";
            this.signup_pw.Size = new System.Drawing.Size(300, 40);
            this.signup_pw.TabIndex = 5;
            this.signup_pw.TextChanged += new System.EventHandler(this.signup_pw_TextChange);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(16, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 37);
            this.label4.TabIndex = 7;
            this.label4.Text = "패스워드 확인";
            // 
            // signup_btn
            // 
            this.signup_btn.Location = new System.Drawing.Point(23, 563);
            this.signup_btn.Name = "signup_btn";
            this.signup_btn.Size = new System.Drawing.Size(445, 50);
            this.signup_btn.TabIndex = 7;
            this.signup_btn.Text = "회원가입";
            this.signup_btn.Click += new System.EventHandler(this.signup_btn_Click);
            // 
            // nn_dupl_chk_btn
            // 
            this.nn_dupl_chk_btn.Location = new System.Drawing.Point(356, 114);
            this.nn_dupl_chk_btn.Name = "nn_dupl_chk_btn";
            this.nn_dupl_chk_btn.Size = new System.Drawing.Size(102, 40);
            this.nn_dupl_chk_btn.TabIndex = 2;
            this.nn_dupl_chk_btn.Text = "중복 확인";
            this.nn_dupl_chk_btn.Click += new System.EventHandler(this.nn_dupl_chk_btn_Click);
            // 
            // close_btn
            // 
            this.close_btn.Location = new System.Drawing.Point(23, 633);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(445, 50);
            this.close_btn.TabIndex = 8;
            this.close_btn.Text = "취소";
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // signup_pw2_warning_lb
            // 
            this.signup_pw2_warning_lb.AutoSize = true;
            this.signup_pw2_warning_lb.ForeColor = System.Drawing.Color.Red;
            this.signup_pw2_warning_lb.Location = new System.Drawing.Point(23, 498);
            this.signup_pw2_warning_lb.Name = "signup_pw2_warning_lb";
            this.signup_pw2_warning_lb.Size = new System.Drawing.Size(0, 25);
            this.signup_pw2_warning_lb.TabIndex = 9;
            // 
            // signup_pw_warning_lb
            // 
            this.signup_pw_warning_lb.AutoSize = true;
            this.signup_pw_warning_lb.ForeColor = System.Drawing.Color.Red;
            this.signup_pw_warning_lb.Location = new System.Drawing.Point(23, 369);
            this.signup_pw_warning_lb.Name = "signup_pw_warning_lb";
            this.signup_pw_warning_lb.Size = new System.Drawing.Size(0, 25);
            this.signup_pw_warning_lb.TabIndex = 10;
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 706);
            this.Controls.Add(this.signup_pw_warning_lb);
            this.Controls.Add(this.signup_pw2_warning_lb);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.nn_dupl_chk_btn);
            this.Controls.Add(this.signup_btn);
            this.Controls.Add(this.signup_pw);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.id_dupl_chk_btn);
            this.Controls.Add(this.signup_id);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.signup_pw2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.signup_name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SignUpForm";
            this.Text = "오목 - 회원가입";
            this.Load += new System.EventHandler(this.SignUpForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox signup_name;
        private MetroFramework.Controls.MetroTextBox signup_pw2;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox signup_id;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroButton id_dupl_chk_btn;
        private MetroFramework.Controls.MetroTextBox signup_pw;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroButton signup_btn;
        private MetroFramework.Controls.MetroButton nn_dupl_chk_btn;
        private MetroFramework.Controls.MetroButton close_btn;
        private System.Windows.Forms.Label signup_pw2_warning_lb;
        private System.Windows.Forms.Label signup_pw_warning_lb;
    }
}