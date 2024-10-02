using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Button button;

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 800);
            this.Text = "Фиолетовая звезда";

            this.DoubleBuffered = true;
            button = new Button();
            button.Text = "Открыть новую форму";
            button.Size = new Size(200, 50);
            button.Location = new Point(
                (this.ClientSize.Width - button.Width) / 2,
                (this.ClientSize.Height - button.Height) / 2
            );
            button.Click += new EventHandler(Button_Click);
            this.Controls.Add(button);

            this.Paint += new PaintEventHandler(MainForm_Paint);

            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetFormToStar();
        }

        private void SetFormToStar()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(CreateStarPoints(
                num_points: 5,
                outer_radius: 300,
                inner_radius: 150,
                cx: this.ClientSize.Width / 2,
                cy: this.ClientSize.Height / 2
            ));
            this.Region = new Region(path);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = Brushes.Violet;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPolygon(brush, CreateStarPoints(
                num_points: 5,
                outer_radius: 300,
                inner_radius: 150,
                cx: this.ClientSize.Width / 2,
                cy: this.ClientSize.Height / 2
            ));
        }

        private PointF[] CreateStarPoints(int num_points, float outer_radius, float inner_radius, float cx, float cy)
        {
            PointF[] pts = new PointF[num_points * 2];
            double step = Math.PI / num_points;
            double angle = -Math.PI / 2;

            for (int i = 0; i < num_points * 2; i++)
            {
                float radius = (i % 2 == 0) ? outer_radius : inner_radius;
                pts[i] = new PointF(
                    (float)(cx + radius * Math.Cos(angle)),
                    (float)(cy + radius * Math.Sin(angle))
                );
                angle += step;
            }
            return pts;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}