using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pack_Server;

namespace Omok
{
    public partial class GoBoardControl : UserControl
    {
        int gridSize;
        int flowerSize = 10;

        enum STONE { none, black, white };
        STONE[,] stones = new STONE[19, 19];
        bool flag = false;  // false = black, true = white
        bool gameDone = false;

        Graphics g;
        Pen pen;
        Brush wBrush, bBrush;


        public event Action<string> OnTurnChanged;

        public NetworkStream NetStream {  get; set; }
        public string MyNick { get; set; }

        public string MyId { get; set; }

        public string CurrentTurnPlayer { get; set; } 


        public GoBoardControl()
        {
            InitializeComponent();

            pen = new Pen(Color.Black);
            bBrush = new SolidBrush(Color.Black);
            wBrush = new SolidBrush(Color.White);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int boardLines = 19;
            int usableWidth = this.ClientSize.Width;
            int usableHeight = this.ClientSize.Height;

            int padding = 10;
            int boardPixelSize = Math.Min(usableWidth, usableHeight) - 2 * padding;
            gridSize = boardPixelSize / (boardLines - 1);
            int actualBoardSize = gridSize * (boardLines - 1);

            int originX = (usableWidth - actualBoardSize) / 2;
            int originY = (usableHeight - actualBoardSize) / 2;

            DrawBoard(e.Graphics, originX, originY);
        }

        private void DrawBoard(Graphics g, int originX, int originY)
        {
            Pen pen = Pens.Black;
            Brush bBrush = Brushes.Black;

            // 선 그리기
            for (int i = 0; i < 19; i++)
            {
                int x = originX + i * gridSize;
                g.DrawLine(pen, x, originY, x, originY + gridSize * 18);

                int y = originY + i * gridSize;
                g.DrawLine(pen, originX, y, originX + gridSize * 18, y);
            }

            // 화점
            for (int x = 3; x <= 15; x += 6)
            {
                for (int y = 3; y <= 15; y += 6)
                {
                    g.FillEllipse(bBrush,
                        originX + gridSize * x - flowerSize / 2,
                        originY + gridSize * y - flowerSize / 2,
                        flowerSize, flowerSize);
                }
            }

            this.BackColor = Color.Orange;

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (stones[i, j] == STONE.none)
                        continue;
                    Brush brush = (stones[i, j] == STONE.black) ? bBrush : wBrush;

                    int cx = originX + i * gridSize;
                    int cy = originY + j * gridSize;

                    g.FillEllipse(brush, cx - gridSize / 2 + 1, cy - gridSize / 2 + 1, gridSize - 2, gridSize - 2);
                    g.DrawEllipse(pen, cx - gridSize / 2 + 1, cy - gridSize / 2 + 1, gridSize - 2, gridSize - 2);
                }

            }
        }

        private bool CheckWin(int x, int y, STONE color)        // 승리 조건 확인
        {
            int[][] directions = new int[][]
            {
                new int[] { 1, 0 }, 
                new int[] { 0, 1 }, 
                new int[] { 1, 1 }, 
                new int[] { 1, -1 } 
            };
            foreach (var dir in directions)
            {
                int count = 1;

                // 정방향
                count += CountStones(x, y, dir[0], dir[1], color);
                // 반대방향
                count += CountStones(x, y, -dir[0], -dir[1], color);

                if (count >= 5)
                    return true;
            }

            return false;

        }
        private int CountStones(int x, int y, int dx, int dy, STONE color)
        {
            int count = 0;
            for (int i = 1; i < 5; i++)
            {
                int nx = x + dx * i;
                int ny = y + dy * i;
                if (nx < 0 || ny < 0 || nx >= 19 || ny >= 19)
                    break;
                if (stones[nx, ny] != color)
                    break;
                
                count++;
            }
            return count;

        }

        public void ApplyRemoteMove(PlaceStonePacket ps)
        {
            stones[ps.x, ps.y] = ps.isBlack ? STONE.black : STONE.white;
            flag = ps.isBlack ? true : false;
            CurrentTurnPlayer = ps.nextTurnPlayer;
            Debug.WriteLine($"[DEBUG] 착수 수신 → 다음 턴: {CurrentTurnPlayer}");
            Invalidate();
            OnTurnChanged?.Invoke(flag ? "( 백 )" : "( 흑 )");
            

        }



        protected override void OnMouseClick(MouseEventArgs e)
        {
            Console.WriteLine($"[DEBUG] MyId : {MyId} ,MyNick: {MyNick}, CurrentTurnPlayer: {CurrentTurnPlayer}");


            if (MyId != CurrentTurnPlayer)
            {
                MessageBox.Show("자기 차례에만 착수 가능");
                return;
            }

            base.OnMouseClick(e);
            if (gameDone) return;
            int boardLines = 19;
            int padding = 10;
            int usableWidth = this.ClientSize.Width;
            int usableHeight = this.ClientSize.Height;

            int boardPixelSize = Math.Min(usableWidth, usableHeight) - 2 * padding;
            gridSize = boardPixelSize / (boardLines - 1);
            int actualBoardSize = gridSize * (boardLines - 1);

            int originX = (usableWidth - actualBoardSize) / 2;
            int originY = (usableHeight - actualBoardSize) / 2;

            // 클릭 위치 → 바둑판 좌표 변환
            int x = (e.X - originX + gridSize / 2) / gridSize;
            int y = (e.Y - originY + gridSize / 2) / gridSize;


            Console.WriteLine($"[DEBUG] OnMouseClick: NetStream=={NetStream == null}, x={x}, y={y}, flag={flag}");

            if (x >= 0 && x < 19 && y >= 0 && y < 19)
            {
                if (stones[x, y] == STONE.none)
                {
                    stones[x, y] = flag ? STONE.white : STONE.black;

                    if (NetStream != null)
                    {
                        var pkt = new PlaceStonePacket
                        {
                            x = x,
                            y = y,
                            isBlack = !flag,      // flag 토글 전이므로 반대
                            player = MyNick
                        };
                        byte[] buf = Packet.Serialize(pkt);
                        NetStream.Write(buf, 0, buf.Length);
                    }


                    flag = !flag;
                    Invalidate();  // 다시 그리기 요청
                    if (CheckWin(x, y, stones[x, y]))
                    {
                        string winner = (stones[x, y] == STONE.black) ? "흑" : "백";
                        MessageBox.Show($"{winner} 승리!", "게임 종료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gameDone = true;
                        //OnTurnChanged?.Invoke("게임 종료");
                        return;
                    }

                   // OnTurnChanged?.Invoke(flag ? "( 백 )" : "( 흑 )");

                }

            }
        }
    }
}
