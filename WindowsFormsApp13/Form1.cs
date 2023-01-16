using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);
            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);
            int x3 = Convert.ToInt32(textBox5.Text);
            int y3 = Convert.ToInt32(textBox6.Text);

            // Create a new Bitmap object and a Graphics object from the image
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draw the triangle
            Point[] points = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
            g.DrawPolygon(Pens.LightBlue, points);

            // Set the Image property of the PictureBox control to the Bitmap object
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the values entered in the textboxes
            int x = Convert.ToInt32(textBox7.Text);
            int y = Convert.ToInt32(textBox8.Text);
            int radius = Convert.ToInt32(textBox9.Text);

            // Create a new Bitmap object and a Graphics object from the image
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);

            
            g.DrawEllipse(Pens.PaleVioletRed, x - radius, y - radius, 2 * radius, 2 * radius);

            // Set the Image property of the PictureBox control to the Bitmap object
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Get the values entered in the textboxes
            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);
            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);
            int x3 = Convert.ToInt32(textBox5.Text);
            int y3 = Convert.ToInt32(textBox6.Text);
            int x = Convert.ToInt32(textBox7.Text);
            int y = Convert.ToInt32(textBox8.Text);
            int radius = Convert.ToInt32(textBox9.Text);

            // Create a new Bitmap object and a Graphics object from the image
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draw the triangle and the circle
            Point[] points = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
            g.DrawPolygon(new Pen(Color.LightBlue, 2), points);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x - radius, y - radius, 2 * radius, 2 * radius);
            // Iterați prin fiecare pixel al Bitmap-ului
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    // Verificați dacă pixelul se află în intersecția celor două forme
                    if (IsIntersection(points, x, y, radius, i, j))
                    {
                        //Schimbați culoarea pixelului în violet
                        bmp.SetPixel(i, j, Color.Purple);
                    }
                }
            }

            // Set the Image property of the PictureBox control to the Bitmap object
            pictureBox1.Image = bmp;
        }
        private void MidPoint(int x, int y, int radius, Graphics g)
        {
            int x_centru = 0;
            int y_centru = radius;
            int p = 5 / 4 - radius;

            while (x_centru <= y_centru)
            {

                if (p < 0)
                {
                    p += 2 * x_centru + 3;
                    x++;
                }
                else
                {
                    p += 2 * (x_centru - y_centru) + 5;
                    y_centru--;
                    x_centru++;
                }
                drawPointCircle(x, y, x_centru, y_centru,g);
            }
        }

        private void drawPointCircle(int x, int y, int x_centru, int y_centru, Graphics g)
        {
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x - x_centru, y - y_centru, 2 * x_centru, 2 * y_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x - y_centru, y - x_centru, 2 * y_centru, 2 * x_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x - x_centru, y + y_centru, 2 * x_centru, 2 * y_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x - y_centru, y + x_centru, 2 * y_centru, 2 * x_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x + x_centru, y - y_centru, 2 * x_centru, 2 * y_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x + y_centru, y - x_centru, 2 * y_centru, 2 * x_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x + x_centru, y + y_centru, 2 * x_centru, 2 * y_centru);
            g.DrawEllipse(new Pen(Color.PaleVioletRed, 2), x + y_centru, y + x_centru, 2 * y_centru, 2 * x_centru);
        }
        bool IsIntersection(Point[] points, int x, int y, int radius, int i, int j)
        {
            // Verificați dacă pixelul este în interiorul triunghiului
            bool insideTriangle = IsInsideTriangle(points, i, j);
            // Verificați dacă pixelul este în interiorul cercului
            bool insideCircle = IsInsideCircle(x, y, radius, i, j);
            // Returnează adevărat dacă pixelul este atât în ​​interiorul triunghiului, cât și al cercului
            return insideTriangle && insideCircle;
        }

        bool IsInsideTriangle(Point[] points, int i, int j)
        {
            // Implementați codul pentru a verifica dacă pixelul se află în interiorul triunghiului
            // folosind metoda coordonatelor baricentrice
            // ...
            double denominator = ((points[1].Y - points[2].Y) * (points[0].X - points[2].X) + (points[2].X - points[1].X) * (points[0].Y - points[2].Y));
            double a = ((points[1].Y - points[2].Y) * (i - points[2].X) + (points[2].X - points[1].X) * (j - points[2].Y)) / denominator;
            double b = ((points[2].Y - points[0].Y) * (i - points[2].X) + (points[0].X - points[2].X) * (j - points[2].Y)) / denominator;
            double c = 1 - a - b;
            return 0 <= a && a <= 1 && 0 <= b && b <= 1 && 0 <= c && c <= 1;
        }

        bool IsInsideCircle(int x, int y, int radius, int i, int j)
        {
            // Verificați dacă distanța dintre pixel și centrul cercului este mai mică sau egală cu raza
            return (i - x) * (i - x) + (j - y) * (j - y) <= radius * radius;
        }

        
    }  
    
}
