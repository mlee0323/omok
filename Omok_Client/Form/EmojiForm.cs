using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Omok_Client.Form
{
    public partial class EmojiForm : MetroForm
    {
        public string SelectedEmoji { get; private set; }
        private string imgPath;

        public EmojiForm()
        {
            InitializeComponent();
            imgPath = Path.Combine(Application.StartupPath, "img");
            if (!Directory.Exists(imgPath))
            {
                imgPath = Path.Combine(Application.StartupPath, @"..\..\img");
                imgPath = Path.GetFullPath(imgPath);
            }
            LoadEmojis();
        }

        private void LoadEmojis()
        {
            this.Size = new Size(400, 400);
            this.Text = "이모지 선택";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                AutoSize = true
            };

            for (int i = 1; i <= 16; i++)
            {
                string imagePath = Path.Combine(imgPath, $"{i}.png");
                if (File.Exists(imagePath))
                {
                    PictureBox pb = new PictureBox
                    {
                        Size = new Size(80, 80),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(imagePath),
                        Tag = $"{i}.png"
                    };

                    pb.DoubleClick += PictureBox_DoubleClick;
                    pb.Cursor = Cursors.Hand;

                    int row = (i - 1) / 4;
                    int col = (i - 1) % 4;
                    panel.Controls.Add(pb, col, row);
                }
            }

            this.Controls.Add(panel);
        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (sender is PictureBox pb && pb.Tag is string fileName)
            {
                SelectedEmoji = fileName;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
} 