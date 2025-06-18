using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Omok_Client.Network;
using Omok_Client.Util;

namespace Omok_Client.Control
{
    public partial class GoBoardControl : UserControl
    {
        public enum STONE { none, black, white }
        private STONE[,] stones = new STONE[19, 19];
        private STONE curStone = STONE.black;
        private int gridSize = 30;
        private bool gameStarted = false;
        private bool gameDone = false;
        private string myTeam = "A";
        private string currentTurnTeam = "A";

        const int flowerSize = 6;

        public string MyTeam
        {
            get => myTeam;
            set => myTeam = value;
        }

        public GoBoardControl()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public void StartGame(string startingTeam)
        {
            gameStarted = true;
            gameDone = false;
            currentTurnTeam = startingTeam;
            stones = new STONE[19, 19];
            Invalidate();
        }

        public void EndGame()
        {
            gameStarted = false;
            gameDone = true;
            Invalidate();
        }

        public void SetCurrentTurn(string team)
        {
            currentTurnTeam = team;
            Invalidate();
        }

        public void PlaceStone(int x, int y, int team)
        {
            if (x < 0 || x >= 19 || y < 0 || y >= 19) return;
            if (stones[x, y] != STONE.none) return;

            stones[x, y] = curStone;
            curStone = (curStone == STONE.black) ? STONE.white : STONE.black;
            Invalidate();
        }

        private int CountStones()
        {
            int count = 0;
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (stones[i, j] != STONE.none)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void RandomizeBoard()
        {
            Random random = new Random();
            int currentStoneCount = CountStones();
            
            // 현재 돌들의 색상 정보 저장
            List<STONE> currentStones = new List<STONE>();
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (stones[i, j] != STONE.none)
                    {
                        currentStones.Add(stones[i, j]);
                    }
                }
            }
            
            // 기존 돌 제거
            stones = new STONE[19, 19];
            
            // 현재 돌 개수만큼 랜덤 배치 (색상 유지)
            for (int i = 0; i < currentStones.Count; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(0, 19);
                    y = random.Next(0, 19);
                } while (stones[x, y] != STONE.none);

                stones[x, y] = currentStones[i];
            }
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (!gameStarted || gameDone) return;
            if (currentTurnTeam != myTeam) return;

            int padding = 10;
            int usableWidth = this.ClientSize.Width;
            int usableHeight = this.ClientSize.Height;
            int boardSize = Math.Min(usableWidth, usableHeight) - 2 * padding;
            gridSize = boardSize / 18;
            int originX = (usableWidth - boardSize) / 2;
            int originY = (usableHeight - boardSize) / 2;

            int x = (e.X - originX + gridSize / 2) / gridSize;
            int y = (e.Y - originY + gridSize / 2) / gridSize;

            if (x >= 0 && x < 19 && y >= 0 && y < 19 && stones[x, y] == STONE.none)
            {
                Client.Send($"STONE_PUT|{Session.Pk}|{Session.Nickname}|{x}|{y}");
            }
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

            // 여기서 호출됨
            DrawBoard(e.Graphics, originX, originY);
        }


        private void DrawBoard(Graphics g, int originX, int originY)
        {
            int boardSize = gridSize * 18;
            Pen pen = Pens.Black;
            Brush bBrush = Brushes.Black;
            Brush wBrush = Brushes.White;
            Brush background = new SolidBrush(Color.Orange);

            // 1. 배경 칠하기
            g.FillRectangle(background, originX, originY, boardSize, boardSize);

            // 2. 선 그리기
            for (int i = 0; i < 19; i++)
            {
                int x = originX + i * gridSize;
                g.DrawLine(pen, x, originY, x, originY + gridSize * 18);

                int y = originY + i * gridSize;
                g.DrawLine(pen, originX, y, originX + gridSize * 18, y);
            }

            // 3. 화점 그리기 (3,3), (3,9), (3,15) 등
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

            // 4. 돌 그리기
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (stones[i, j] == STONE.none) continue;

                    Brush brush = (stones[i, j] == STONE.black) ? bBrush : wBrush;
                    int cx = originX + i * gridSize;
                    int cy = originY + j * gridSize;

                    g.FillEllipse(brush, cx - gridSize / 2 + 1, cy - gridSize / 2 + 1, gridSize - 2, gridSize - 2);
                    g.DrawEllipse(pen, cx - gridSize / 2 + 1, cy - gridSize / 2 + 1, gridSize - 2, gridSize - 2);
                }
            }
        }

        // 히스토리용
        public void ClearBoard()
        {
            // 내부 stones 배열 초기화
            var field = typeof(GoBoardControl).GetField("stones", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var arr = new STONE[19, 19];
            field.SetValue(this, arr);
            Invalidate();
        }

        public void SetStone(int x, int y, STONE stone)
        {
            var field = typeof(GoBoardControl).GetField("stones", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var arr = (STONE[,])field.GetValue(this);
            if (x < 0 || x >= 19 || y < 0 || y >= 19) return;
            arr[x, y] = stone;
            Invalidate();
        }
    }
}
