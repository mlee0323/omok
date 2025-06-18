using MetroFramework.Forms;
using Omok_Client.Control;
using Omok_Client.Network;
using Omok_Client.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Omok_Client.Form
{
    public partial class History : MetroForm
    {
        private GoBoardControl board; // 바둑판 컨트롤
        private class StoneInfo
        {
            public int Turn;
            public int X, Y;
            public char Color;
            public int Skill;
            public string User;
        }
        private int gamePk;
        private List<StoneInfo> currentStones = new List<StoneInfo>();
        private int currentIndex;

        public History()
        {
            InitializeComponent();

            if (!Client.IsConnected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.");
                return;
            }

            // 바둑판 로드
            LoadBadukpan();

            lbl_put.Text = "";
            lbl_skill3.Visible = false;

            btn_prev.Enabled = false;
            btn_next.Enabled = false;
        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadGames();
        }

        private void LoadBadukpan()
        {
            // 바둑판 컨트롤 로드
            board = new GoBoardControl();
            board.Dock = DockStyle.Fill;
            pn_board.Controls.Add(board);

            lbl_put.BackColor = Color.Transparent;
            lbl_put.AutoSize = true;
            lbl_put.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void LoadGames()
        {
            try
            {
                lv_game.Items.Clear();
                using (var hist = new TcpClient(Client.GetHost(), Client.GetPort()))
                using (var writer = new StreamWriter(hist.GetStream(), Encoding.UTF8) { AutoFlush = true })
                using (var reader = new StreamReader(hist.GetStream(), Encoding.UTF8))
                {
                    writer.WriteLine($"HISTORY_LOAD|{Session.Pk}");
                    string msg;
                    while ((msg = reader.ReadLine()) != null)
                    {
                        if (msg == "HISTORY_LOAD_END") break;
                        if (!msg.StartsWith("HISTORY_GAME|")) continue;

                        var t = msg.Split('|');

                        // 승, 무, 패 파싱
                        string win = "";
                        switch (t[5][0])
                        {
                            case 'W':
                                win = "승리";
                                break;
                            case 'D':
                                win = "무승부";
                                break;
                            case 'L':
                                win = "패배";
                                break;
                        }

                        // 시간 파싱
                        var start = DateTime.ParseExact(t[3].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        var end = DateTime.ParseExact(t[4].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        string startDate = start.ToString("yyyy년 M월 d일 HH:mm");
                        string endTime = end.ToString("yyyy년 M월 d일 HH:mm");

                        var item = new ListViewItem(new[] { t[2], win, startDate, endTime });
                        item.Tag = int.Parse(t[1]);
                        item.UseItemStyleForSubItems = false;
                        switch (t[5])
                        {
                            case "W": item.SubItems[1].ForeColor = Color.Green; break;
                            case "D": item.SubItems[1].ForeColor = Color.DarkGray; break;
                            case "L": item.SubItems[1].ForeColor = Color.Red; break;
                        }
                        lv_game.Items.Add(item);
                    }
                }
                foreach (ColumnHeader col in lv_game.Columns)
                    col.Width = -2;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lv_game_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_game.SelectedItems.Count == 0) return;
            var sel = lv_game.SelectedItems[0];
            
            if (gamePk == (int)sel.Tag) return;
            
            gamePk = (int)sel.Tag;
            LoadStones(gamePk);
            if (currentStones.Count > 0)
            {
                currentIndex = 0;
                RefreshBoard();
            }
        }

        private void LoadStones(int gamePk)
        {
            try
            {
                currentStones.Clear();

                using (var hist = new TcpClient(Client.GetHost(), Client.GetPort()))
                using (var writer = new StreamWriter(hist.GetStream(), Encoding.UTF8) { AutoFlush = true })
                using (var reader = new StreamReader(hist.GetStream(), Encoding.UTF8))
                {
                    writer.WriteLine($"HISTORY_STONES_LOAD|{gamePk}");
                    string msg;
                    while ((msg = reader.ReadLine()) != null)
                    {
                        if (msg == "HISTORY_STONES_LOAD_END") break;
                        if (msg.StartsWith("HISTORY_STONE|"))
                        {
                            var t = msg.Split('|');
                            currentStones.Add(new StoneInfo
                            {
                                Turn = int.Parse(t[1]),
                                X = int.Parse(t[3]),
                                Y = int.Parse(t[4]),
                                Color = t[5][0],
                                Skill = int.Parse(t[6]),
                                User = t[2]
                            });
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"돌 기록 로딩 중 네트워크 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshBoard()
        {
            board.ClearBoard();
            lbl_skill3.Visible = false;
            for (int i = 0; i <= currentIndex && i < currentStones.Count; i++)
            {
                var s = currentStones[i];
                board.SetStone(s.X, s.Y, s.Color, s.Skill);
            }
            
            btn_prev.Enabled = currentIndex > 0;
            btn_next.Enabled = currentIndex < currentStones.Count - 1;

            lbl_put.Text = string.Empty;
            if (currentIndex >= 0 && currentIndex < currentStones.Count)
            {
                lbl_put.Text = $"턴 {currentStones[currentIndex].Turn}: {currentStones[currentIndex].User}";

                switch (currentStones[currentIndex].Skill)
                {
                    case 0:
                        lbl_put.Text += $" ({currentStones[currentIndex].X}, {currentStones[currentIndex].Y}) 착수";
                        break;
                    case 1:
                        lbl_put.Text += $" 지우기 스킬 발동! ({currentStones[currentIndex].X}, {currentStones[currentIndex].Y}) 삭제";
                        break;
                    case 3:
                        lbl_put.Text += $" 엎기 스킬 발동!";
                        lbl_skill3.Visible = true;
                        break;
                }
            }   
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            RefreshBoard();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (currentIndex < currentStones.Count - 1) currentIndex++;
            RefreshBoard();
        }
    }
}
