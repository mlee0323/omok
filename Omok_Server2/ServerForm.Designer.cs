namespace Omok_Server2
{
    partial class ServerForm
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.btn_server = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.threadList = new System.Windows.Forms.ListView();
            this.threadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ThreadStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_exit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(23, 83);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(320, 344);
            this.txtLog.TabIndex = 0;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(24, 64);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(29, 13);
            this.label.TabIndex = 1;
            this.label.Text = "로그";
            // 
            // btn_server
            // 
            this.btn_server.Location = new System.Drawing.Point(351, 400);
            this.btn_server.Name = "btn_server";
            this.btn_server.Size = new System.Drawing.Size(87, 27);
            this.btn_server.TabIndex = 2;
            this.btn_server.Text = "서버 시작";
            this.btn_server.UseVisualStyleBackColor = true;
            this.btn_server.Click += new System.EventHandler(this.btn_server_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.threadList);
            this.groupBox1.Location = new System.Drawing.Point(351, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 311);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thread 상태";
            // 
            // threadList
            // 
            this.threadList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.threadName,
            this.ThreadStatus});
            this.threadList.HideSelection = false;
            this.threadList.Location = new System.Drawing.Point(0, 20);
            this.threadList.Name = "threadList";
            this.threadList.Size = new System.Drawing.Size(200, 291);
            this.threadList.TabIndex = 0;
            this.threadList.UseCompatibleStateImageBehavior = false;
            this.threadList.View = System.Windows.Forms.View.Details;
            // 
            // threadName
            // 
            this.threadName.Text = "Thread 이름";
            this.threadName.Width = 137;
            // 
            // ThreadStatus
            // 
            this.ThreadStatus.Text = "상태";
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(464, 400);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(87, 27);
            this.btn_exit.TabIndex = 4;
            this.btn_exit.Text = "나가기";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 450);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_server);
            this.Controls.Add(this.label);
            this.Controls.Add(this.txtLog);
            this.Name = "ServerForm";
            this.Text = "서버 관리자";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btn_server;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView threadList;
        private System.Windows.Forms.ColumnHeader threadName;
        private System.Windows.Forms.ColumnHeader ThreadStatus;
        private System.Windows.Forms.Button btn_exit;
    }
}

