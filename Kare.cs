using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDP_proje
{
    public class Kare
    {
        public static void DrawRectangle(Form2.Rectangle kare, PaintEventArgs e)
        {
            switch (kare.direction_Kare)
            {                
                case Form2.Direction_Kare.One:
                    if (kare.end.X>=605)
                    {
                        kare.end.X = 605;
                    }
                    e.Graphics.FillRectangle(kare.color, new Rectangle(new Point(kare.start.X,
                        kare.end.Y), new Size(kare.end.X - kare.start.X,
                            kare.start.Y - kare.end.Y)));
                    break;
                case Form2.Direction_Kare.Two:
                    e.Graphics.FillRectangle(kare.color, new Rectangle(kare.end,
                        new Size(kare.start.X - kare.end.X,
                            kare.start.Y - kare.end.Y)));
                    break;
                case Form2.Direction_Kare.Three:
                    if (kare.end.Y >= 509)
                    {
                        kare.end.Y = 509;
                    }
                    e.Graphics.FillRectangle(kare.color, new Rectangle(new Point(kare.end.X,
                        kare.start.Y), new Size(kare.start.X - kare.end.X,
                            kare.end.Y - kare.start.Y)));
                    break;
                case Form2.Direction_Kare.Four:
                    if (kare.end.X >= 602)
                    {
                        kare.end.X = 602;
                    }
                    if (kare.end.Y >= 509)
                    {
                        kare.end.Y = 509;
                    }
                    e.Graphics.FillRectangle(kare.color, new Rectangle(kare.start,
                        new Size(kare.end.X - kare.start.X,
                            kare.end.Y - kare.start.Y)));
                    break;
                default:
                    MessageBox.Show("Hata");
                    break;
            }
            
            
        }
        public static void DetermineDirection_kare(MouseEventArgs e)
        {
            if (e.Y < 0 && e.X < 0)
            {
                Form2.mkare.end.X = 0;
                Form2.mkare.end.Y = 0;
            }
            else if (e.X < 0)
            {
                Form2.mkare.end.X = 0;
                Form2.mkare.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                Form2.mkare.end.X = e.X;
                Form2.mkare.end.Y = 0;
            }
            else
                Form2.mkare.end = e.Location;

            if (Form2.mkare.end.X > Form2.mkare.start.X && Form2.mkare.end.Y <= Form2.mkare.start.Y)
                Form2.mkare.direction_Kare = Form2.Direction_Kare.One;
            else if (Form2.mkare.end.X <= Form2.mkare.start.X && Form2.mkare.end.Y < Form2.mkare.start.Y)
                Form2.mkare.direction_Kare = Form2.Direction_Kare.Two;
            else if (Form2.mkare.end.X < Form2.mkare.start.X && Form2.mkare.end.Y >= Form2.mkare.start.Y)
                Form2.mkare.direction_Kare = Form2.Direction_Kare.Three;
            else if (Form2.mkare.end.X >= Form2.mkare.start.X && Form2.mkare.end.Y > Form2.mkare.start.Y)
                Form2.mkare.direction_Kare = Form2.Direction_Kare.Four;
        }
    }
}

