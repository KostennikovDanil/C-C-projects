using Curves;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Curves;

namespace UI
{

    public partial class UIWP : Form
    {
        readonly Pen penSystem;
        readonly Pen penDraw;
        public Graphics Graphic { get; private set; }
        public Bitmap bmp;
        public PictureBox pictureBox;
        public Point[] PositivePoints { get; private set; }
        public Point[] NegativePoints { get; private set; }

        public UIWP()
        {
            InitializeComponent();
            penSystem = new Pen(Brushes.Black, 1f);
            penDraw = new Pen(Color.Red, 3f);

            pictureBox = new PictureBox()
            {
                Dock = DockStyle.Fill,
            };

            Controls.Add(pictureBox);

            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);

            var comboBox = new ComboBox()
            {
                Dock = DockStyle.Top,
            };
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.Add(new Hyperbola());
            comboBox.Items.Add(new Ellips());
            comboBox.Items.Add(new Parabola());

            var scaleTB = new TrackBar()
            {
                Location = new Point(5, ClientSize.Height - 100),
                SmallChange = 1,
                LargeChange = 10,
                TickFrequency = 5,
                Maximum = 500,
                Minimum = 100,
                Parent = pictureBox
            };
            Controls.Add(scaleTB);

            var scalingLabel = new Label()
            {
                Location = new Point(5, ClientSize.Height - 120),
                Size = new Size(scaleTB.Width, 20),
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = "Scaling: 1,0",
            };

            Controls.Add(scalingLabel);

            double scale = 0.5;
            Controls.Add(comboBox);
            DrawSystem(scale);


            scaleTB.Scroll += (sender, args) =>
            {
                scalingLabel.Text = "Scaling: " + (scaleTB.Value / 100f).ToString();
                scale = scaleTB.Value / 100f / 2;
                DrawSystem(scale);
                if (comboBox.SelectedIndex > -1)
                    DrawCurve(comboBox, scale);
            };

            scalingLabel.Parent = pictureBox;
            scaleTB.Parent = pictureBox;

            comboBox.SelectedIndexChanged += (sender, args) =>
            {
                DrawSystem(scale);
                DrawCurve(comboBox, scale);
            };

        }


        public void DrawSystem(double scale)
        {
            Graphic = Graphics.FromImage(bmp);
            Graphic.SmoothingMode = SmoothingMode.HighQuality;
            Graphic.Clear(Color.White);

            Graphic.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);


            Graphic.DrawLine(penSystem, -ClientSize.Width / 2, 0, ClientSize.Width / 2, 0);
            Graphic.DrawLine(penSystem, 0, -ClientSize.Height / 2, 0, ClientSize.Height / 2);
            int step = (int)(ClientSize.Width / 10 * scale);
            for (int i = 0; i < ClientSize.Width / 2; i += step)
            {
                Graphic.DrawLine(penSystem, i, -5, i, 5);
                Graphic.DrawLine(penSystem, -i, -5, -i, 5);
            }
            for (int i = 0; i < ClientSize.Height / 2; i += step)
            {
                Graphic.DrawLine(penSystem, -5, i, 5, i);
                Graphic.DrawLine(penSystem, -5, -i, 5, -i);
            }
            pictureBox.Image = bmp;
        }

        public void DrawCurve(ComboBox comboBox, double scale)
        {
            var region = new Curves.Region(new Point(0, 0), new Point(ClientSize.Width, ClientSize.Height));
            var curve = (Curve)comboBox.SelectedItem;
            curve.Build(region, scale);
            PositivePoints = curve.PositivePoints.ToArray();
            NegativePoints = curve.NegativePoints.ToArray();
            Graphic = Graphics.FromImage(bmp);
            Graphic.SmoothingMode = SmoothingMode.HighQuality;
            Graphic.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            if ((PositivePoints.Count() != 0) && (NegativePoints.Count() != 0))
            {
                if (curve.Name == "Эллипс")
                {
                    Graphic.DrawClosedCurve(penDraw, PositivePoints.Concat(NegativePoints).ToArray());
                }
                else
                {
                    Graphic.DrawCurve(penDraw, PositivePoints);
                    Graphic.DrawCurve(penDraw, NegativePoints);
                }
            }
            Graphic.TranslateTransform(-ClientSize.Width / 2, -ClientSize.Height / 2);
            pictureBox.Image = bmp;

        }
        private void UIWP_Load(object sender, EventArgs e)
        {

        }
    }
}
