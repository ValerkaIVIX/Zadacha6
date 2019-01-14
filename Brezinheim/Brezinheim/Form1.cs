using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Brezinheim;
//using Drawing.cs;

namespace Brezinheim
{
    public partial class Form1 : Form
    {
        Drawing points;

        public Form1()
        {
            InitializeComponent();
            points = new Drawing();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Graphics draw = pictureBox1.CreateGraphics();
            draw.Clear(Color.White);
            draw.Dispose();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DrawPoint(int x, int y)
        {
            Graphics draw = pictureBox1.CreateGraphics();
            draw.ScaleTransform(8, 8);
            draw.FillRectangle(Brushes.Black, x, y, 1, 1);
        }

        void Draw_Brezinheim_Line(int x0, int y0, int x1, int y1)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек

            if (steep)  // Отражаем линию по диагонали, если угол наклона слишком большой
            {
                points.Swap(ref x0, ref y0);
                points.Swap(ref x1, ref y1);
            }

            if (x0 > x1)    // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            {
                points.Swap(ref x0, ref x1);
                points.Swap(ref y0, ref y1);
            }

            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;

            for (int x = x0; x <= x1; x++)
            {
                DrawPoint(steep ? y : x, steep ? x : y);
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                Graphics draw = pictureBox1.CreateGraphics();
                draw.Clear(Color.White);
                int x = e.X / 8;
                int y = e.Y / 8;
                points.p1.X = x;
                points.p1.Y = y;
                draw.Dispose();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (points.p1 != null)
                {
                    int x = e.X / 8;
                    int y = e.Y / 8;
                    points.p2.X = x;
                    points.p2.Y = y;
                    Draw_Brezinheim_Line(points.p1.X, points.p1.Y, points.p2.X, points.p2.Y);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Graphics draw = pictureBox1.CreateGraphics();
                draw.Clear(Color.White);
                draw.Dispose();
                int radius = trackBar1.Value;
                int x = pictureBox1.Width / 16;
                int y = pictureBox1.Height / 16;
                Draw_Brezinheim_Circle(x, y, radius);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void Draw_Brezinheim_Circle(int x0, int y0, int radius)
        {
            int x = radius;
            int y = 0;
            int radiusError = 1 - x;
            while (x >= y)
            {
                DrawPoint(x + x0, y + y0);
                DrawPoint(y + x0, x + y0);
                DrawPoint(-x + x0, y + y0);
                DrawPoint(-y + x0, x + y0);
                DrawPoint(-x + x0, -y + y0);
                DrawPoint(-y + x0, -x + y0);
                DrawPoint(x + x0, -y + y0);
                DrawPoint(y + x0, -x + y0);
                y++;
                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x + 1);
                }
            }
        }


    }
}
