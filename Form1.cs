using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NDP_proje
{

    
    public partial class Form1 : Form 
    {
        public Form1()
        {
            InitializeComponent();
            this.panel1.MouseDown += new MouseEventHandler(Panel1_Down);
            this.panel1.MouseMove += new MouseEventHandler(Panel1_Move);
            this.panel1.MouseUp += new MouseEventHandler(Panel1_Up);
            this.panel1.Paint += new PaintEventHandler(Panel1_Paint);
        }
        int a = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            a++;
            
        }
        
        public void aa()
        {

        }
        
       
        // yön, başlangıç ve bitiş koordinatina göre elipsi çiz
        private void DrawEllipse(Ellipse ellipse, PaintEventArgs e)
        {
            Graphics daire1 = panel1.CreateGraphics();
            SolidBrush firca = new SolidBrush(Color.Red);
            //  daire1.FillEllipse(firca, new Rectangle(new Point(10, 10), new Size(50, 50)));    //örnek böyle
            switch (ellipse.direction)
            {
                case Direction.One:
                    daire1.FillEllipse(firca, new Rectangle(new Point(ellipse.start.X,
                        ellipse.end.Y), new Size(ellipse.end.X - ellipse.start.X,
                            ellipse.start.Y - ellipse.end.Y)));
                    break;
                case Direction.Two:
                    daire1.FillEllipse(firca, new Rectangle(ellipse.end,
                        new Size(ellipse.start.X - ellipse.end.X,
                            ellipse.start.Y - ellipse.end.Y)));
                    break;
                case Direction.Three:
                    daire1.FillEllipse(firca, new Rectangle(new Point(ellipse.end.X,
                        ellipse.start.Y), new Size(ellipse.start.X - ellipse.end.X,
                            ellipse.end.Y - ellipse.start.Y)));
                    break;
                case Direction.Four:
                    daire1.FillEllipse(firca, new Rectangle(ellipse.start,
                        new Size(ellipse.end.X - ellipse.start.X,
                            ellipse.end.Y - ellipse.start.Y)));
                    break;
                default:
                    MessageBox.Show("Hata");
                    break;
            }
        }
       


        //kare
        public void DrawRectangle(Kare.kare rectangle, PaintEventArgs e)
        {
            Graphics daire1 = panel1.CreateGraphics();
            SolidBrush firca = new SolidBrush(Color.Red);
            switch (rectangle.direction)
            {
                case Kare.Direction.One:
                    daire1.FillRectangle(firca, new Rectangle(new Point(rectangle.start.X,
                        rectangle.end.Y), new Size(rectangle.end.X - rectangle.start.X,
                            rectangle.start.Y - rectangle.end.Y)));
                    break;
                case Kare.Direction.Two:
                    daire1.FillRectangle(firca, new Rectangle(rectangle.end,
                        new Size(rectangle.start.X - rectangle.end.X,
                            rectangle.start.Y - rectangle.end.Y)));
                    break;
                case Kare.Direction.Three:
                    daire1.FillRectangle(firca, new Rectangle(new Point(rectangle.end.X,
                        rectangle.start.Y), new Size(rectangle.start.X - rectangle.end.X,
                            rectangle.end.Y - rectangle.start.Y)));
                    break;
                case Kare.Direction.Four:
                    daire1.FillRectangle(firca, new Rectangle(rectangle.start,
                        new Size(rectangle.end.X - rectangle.start.X,
                            rectangle.end.Y - rectangle.start.Y)));
                    break;
                default:
                    MessageBox.Show("Hata");
                    break;
            }
        }


        //Elips yönünü başlangıç ve bitiş koordinatına göre belirle
        private void DetermineDirection(MouseEventArgs e)
        {
            if (e.Y < 0 && e.X < 0)
            {
                mEllipse.end.X = 0;
                mEllipse.end.Y = 0;
            }
            else if (e.X < 0)
            {
                mEllipse.end.X = 0;
                mEllipse.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                mEllipse.end.X = e.X;
                mEllipse.end.Y = 0;
            }
            else
                mEllipse.end = e.Location;

            if (mEllipse.end.X > mEllipse.start.X && mEllipse.end.Y <= mEllipse.start.Y)
                mEllipse.direction = Direction.One;
            else if (mEllipse.end.X <= mEllipse.start.X && mEllipse.end.Y < mEllipse.start.Y)
                mEllipse.direction = Direction.Two;
            else if (mEllipse.end.X < mEllipse.start.X && mEllipse.end.Y >= mEllipse.start.Y)
                mEllipse.direction = Direction.Three;
            else if (mEllipse.end.X >= mEllipse.start.X && mEllipse.end.Y > mEllipse.start.Y)
                mEllipse.direction = Direction.Four;
        }

        private void DetermineDirection_kare(MouseEventArgs e)
        {
            if (e.Y < 0 && e.X < 0)
            {
                mkaree.end.X = 0;
                mkaree.end.Y = 0;
            }
            else if (e.X < 0)
            {
                mkaree.end.X = 0;
                mkaree.end.Y = e.Y;
            }
            else if (e.Y < 0)
            {
                mkaree.end.X = e.X;
                mkaree.end.Y = 0;
            }
            else
                mkaree.end = e.Location;

            if (mkaree.end.X > mkaree.start.X && mkaree.end.Y <= mkaree.start.Y)
                mkaree.direction = Kare.Direction.One;
            else if (mkaree.end.X <= mkaree.start.X && mkaree.end.Y < mkaree.start.Y)
                mkaree.direction = Kare.Direction.Two;
            else if (mkaree.end.X < mkaree.start.X && mkaree.end.Y >= mkaree.start.Y)
                mkaree.direction = Kare.Direction.Three;
            else if (mkaree.end.X >= mkaree.start.X && mkaree.end.Y > mkaree.start.Y)
                mkaree.direction = Kare.Direction.Four;
        }
        /*
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {/*
            if (a>0)
            {
                foreach (Kare kare in mkares)
                    this.DrawRectangle(kare, e);


                if (mDrawing)
                    this.DrawRectangle(mkaree, e);
            }
            else
            {
                foreach (Ellipse ellipse in mEllipses)
                    this.DrawEllipse(ellipse, e);


                if (mDrawing)
                    this.DrawEllipse(mEllipse, e);
            }
        }

        private void Panel1_Move(object sender, MouseEventArgs e)
        {
            if (a>0)
            {
                if (!mDrawingk) return;
                // Mouse hareket ettiğinde yönü belirle tamamen 
                this.DetermineDirection_kare(e);
                this.panel1.Invalidate();
            }
            else
            {
                if (!mDrawing) return;
                // Mouse hareket ettiğinde yönü belirle tamamen 
                this.DetermineDirection(e);
                this.panel1.Invalidate();
            }
        }

        private void Panel1_Down(object sender, MouseEventArgs e)
        {
            if (a>0)
            {
                mkaree.start = e.Location;
                if (e.Button == MouseButtons.Left)
                    mDrawingk = true;
                else
                    this.panel1.Invalidate();
            }
            else
            {
                mEllipse.start = e.Location;
                if (e.Button == MouseButtons.Left)
                    mDrawing = true;
                else
                    this.panel1.Invalidate();
            }
        }

        private void Panel1_Up(object sender, MouseEventArgs e)
        {
            Kare asd = new Kare();
            if (a>0)
            {
                if (!mDrawingk) return;
                mDrawingk = false;
                this.DetermineDirection_kare(e);
                // Yarattığımız elipsi liste al
                mkares.Add(mkaree);
                this.panel1.Invalidate();
            }
            else
            {
                if (!mDrawing) return;
                mDrawing = false;
                this.DetermineDirection(e);
                // Yarattığımız elipsi liste al
                mEllipses.Add(mEllipse);
                this.panel1.Invalidate();
            }
        }
        */
        private void button2_Click(object sender, EventArgs e)
        {
            a--;
        }

        private void Panel1_Down(object sender, MouseEventArgs e)
        {

        }
    }
}
