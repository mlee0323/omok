using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omok
{
    public partial class GoBoardControl : UserControl
    {
        int gridSize;
        int flowerSize = 10;

        Graphics g;
        Pen pen;
        Brush wBrush, bBrush;

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
        }
    }
}
