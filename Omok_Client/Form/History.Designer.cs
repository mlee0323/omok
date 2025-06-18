namespace Omok_Client.Form
{
    partial class History
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
            this.pn_board = new MetroFramework.Controls.MetroPanel();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lv_game = new System.Windows.Forms.ListView();
            this.ch_gamecode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_startTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_endTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_put = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pn_board
            // 
            this.pn_board.HorizontalScrollbarBarColor = true;
            this.pn_board.HorizontalScrollbarHighlightOnWheel = false;
            this.pn_board.HorizontalScrollbarSize = 10;
            this.pn_board.Location = new System.Drawing.Point(24, 224);
            this.pn_board.Name = "pn_board";
            this.pn_board.Size = new System.Drawing.Size(500, 500);
            this.pn_board.TabIndex = 0;
            this.pn_board.VerticalScrollbarBarColor = true;
            this.pn_board.VerticalScrollbarHighlightOnWheel = false;
            this.pn_board.VerticalScrollbarSize = 10;
            // 
            // btn_next
            // 
            this.btn_next.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_next.Location = new System.Drawing.Point(484, 730);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(40, 40);
            this.btn_next.TabIndex = 5;
            this.btn_next.Text = "▶";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_prev.Location = new System.Drawing.Point(438, 730);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(40, 40);
            this.btn_prev.TabIndex = 6;
            this.btn_prev.Text = "◀";
            this.btn_prev.UseVisualStyleBackColor = true;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 72);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(65, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "히스토리";
            // 
            // lv_game
            // 
            this.lv_game.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_gamecode,
            this.ch_result,
            this.ch_startTime,
            this.ch_endTime});
            this.lv_game.HideSelection = false;
            this.lv_game.Location = new System.Drawing.Point(24, 95);
            this.lv_game.Name = "lv_game";
            this.lv_game.Size = new System.Drawing.Size(500, 123);
            this.lv_game.TabIndex = 8;
            this.lv_game.UseCompatibleStateImageBehavior = false;
            this.lv_game.View = System.Windows.Forms.View.Details;
            this.lv_game.SelectedIndexChanged += new System.EventHandler(this.Lv_game_SelectedIndexChanged);
            // 
            // ch_gamecode
            // 
            this.ch_gamecode.Text = "GameCode";
            this.ch_gamecode.Width = 133;
            // 
            // ch_result
            // 
            this.ch_result.Text = "Result";
            this.ch_result.Width = 126;
            // 
            // ch_startTime
            // 
            this.ch_startTime.Text = "Start";
            this.ch_startTime.Width = 111;
            // 
            // ch_endTime
            // 
            this.ch_endTime.Text = "End";
            this.ch_endTime.Width = 119;
            // 
            // lbl_put
            // 
            this.lbl_put.AutoSize = true;
            this.lbl_put.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_put.Location = new System.Drawing.Point(23, 730);
            this.lbl_put.Name = "lbl_put";
            this.lbl_put.Size = new System.Drawing.Size(64, 21);
            this.lbl_put.TabIndex = 10;
            this.lbl_put.Text = "턴 설명";
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 781);
            this.Controls.Add(this.lbl_put);
            this.Controls.Add(this.lv_game);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btn_prev);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.pn_board);
            this.Name = "History";
            this.Text = "전적";
            this.Load += new System.EventHandler(this.History_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pn_board;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_prev;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.ListView lv_game;
        private System.Windows.Forms.ColumnHeader ch_gamecode;
        private System.Windows.Forms.ColumnHeader ch_result;
        private System.Windows.Forms.ColumnHeader ch_startTime;
        private System.Windows.Forms.ColumnHeader ch_endTime;
        private System.Windows.Forms.Label lbl_put;
    }
}