using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDP_proje
{
    public class Yuvarlak
    {
        public static void DrawEllipse(Form2.Ellipse ellipse, PaintEventArgs e)
        {
            switch (ellipse.direction)
            {
                
                case Form2.Direction.One:
                    if (ellipse.end.X >= 602)
                    {
                        ellipse.end.X = 602;
                    }
                    e.Graphics.FillEllipse(ellipse.color, new Rectangle(new Point(ellipse.start.X,
                        ellipse.end.Y), new Size(ellipse.end.X - ellipse.start.X,
                            ellipse.start.Y - ellipse.end.Y)));
                    break;
                case Form2.Direction.Two:
                    e.Graphics.FillEllipse(ellipse.color, new Rectangle(ellipse.end,
                        new Size(ellipse.start.X - ellipse.end.X,
                            ellipse.start.Y - ellipse.end.Y)));
                    break;
                case Form2.Direction.Three:
                    if (ellipse.end.Y >= 509)
                    {
                        ellipse.end.Y = 509;

                    }
                    e.Graphics.FillEllipse(ellipse.color, new Rectangle(new Point(ellipse.end.X,
                        ellipse.start.Y), new Size(ellipse.start.X - ellipse.end.X,
                            ellipse.end.Y - ellipse.start.Y)));
                    break;
                case Form2.Direction.Four:
                    if (ellipse.end.X >= 602)
                    {
                        ellipse.end.X = 602;
                    }
                    if (ellipse.end.Y >= 509)
                    {
                        ellipse.end.Y = 509;

                    }
                    e.Graphics.FillEllipse(ellipse.color, new Rectangle(ellipse.start,
                        new Size(ellipse.end.X - ellipse.start.X,
                            ellipse.end.Y - ellipse.start.Y)));
                    break;
                default:
                    MessageBox.Show("Hata");
                    break;
            }
        }
        public static void DetermineDirection(MouseEventArgs e)
        {
            if (e.Y < 0 && e.X < 0)
            {
                Form2.mEllipse.end.X = 0;
                Form2.mEllipse.end.Y = 0;
            }
            else if (e.X < 0)
            {
                Form2.mEllipse.end.X = 0;
                Form2.mEllipse.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                Form2.mEllipse.end.X = e.X;
                Form2.mEllipse.end.Y = 0;
            }
            else
                Form2.mEllipse.end = e.Location;

            if (Form2.mEllipse.end.X > Form2.mEllipse.start.X && Form2.mEllipse.end.Y <= Form2.mEllipse.start.Y)
                Form2.mEllipse.direction = Form2.Direction.One;
            else if (Form2.mEllipse.end.X <= Form2.mEllipse.start.X && Form2.mEllipse.end.Y < Form2.mEllipse.start.Y)
                Form2.mEllipse.direction = Form2.Direction.Two;
            else if (Form2.mEllipse.end.X < Form2.mEllipse.start.X && Form2.mEllipse.end.Y >= Form2.mEllipse.start.Y)
                Form2.mEllipse.direction = Form2.Direction.Three;
            else if (Form2.mEllipse.end.X >= Form2.mEllipse.start.X && Form2.mEllipse.end.Y > Form2.mEllipse.start.Y)
                Form2.mEllipse.direction = Form2.Direction.Four;
        }
    }
}
