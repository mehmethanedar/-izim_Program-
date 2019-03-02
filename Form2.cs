using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDP_proje
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            this.panel1.MouseDown += new MouseEventHandler(Panel1_MouseDown);
            this.panel1.MouseMove += new MouseEventHandler(Panel1_MouseMove);
            this.panel1.MouseUp += new MouseEventHandler(Panel1_MouseUp);
            this.panel1.Paint += new PaintEventHandler(Panel1_Paint);
            mEllipses = new List<Ellipse>();
            yedeks = new List<Yedek>();
            mkares = new List<Rectangle>();
        }
        public string sekil = "";
        public string secilen = "";
        public SolidBrush MyBrush;
        public Point secim;
        public struct Yedek
        {
            // Mouse'un basıldığı koordinat
            public Point start;
            // Mouse'un bırakıldığı koordinat
            public Point end;
            public Direction_Yedek direction_Yedek;
            public SolidBrush color;
        }
        public List<Yedek> yedeks;
        public Yedek yedek;
        public enum Direction_Yedek
        {
            One,
            Two,
            Three,
            Four
        }



        public struct Ellipse
        {
            // Mouse'un basıldığı koordinat
            public Point start;
            // Mouse'un bırakıldığı koordinat
            public Point end;
            // yön belirlemek için
            public Direction direction;

            public SolidBrush color;

        }
        // 4lü koordinat quadrantı
        public enum Direction
        {
            One,
            Two,
            Three,
            Four
        }
        // tüm elipsleri depola
        public List<Ellipse> mEllipses;
        // şuan çizilen elips
        public static Ellipse mEllipse;
        // eğer Mouse basılı ise...
        public bool mDrawing;

        //kare
        public struct Rectangle
        {
            // Mouse'un basıldığı koordinat
            public Point start;
            // Mouse'un bırakıldığı koordinat
            public Point end;
            // yön belirlemek için
            public Direction_Kare direction_Kare;

            public SolidBrush color;


        }
        // 4lü koordinat quadrantı
        public enum Direction_Kare
        {
            One,
            Two,
            Three,
            Four
        }
        // tüm elipsleri depola
        public  List<Rectangle> mkares;
        // şuan çizilen elips
        public static Rectangle mkare;
        // eğer Mouse basılı ise...
        public  bool mDrawing_kare;


        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {

            switch (sekil)
            {
                case "kare":
                    if (!mDrawing_kare) return;
                    // Mouse hareket ettiğinde yönü belirle tamamen 
                    Kare.DetermineDirection_kare(e);
                    this.panel1.Invalidate();
                    break;
                case "yuvarlak":
                    if (!mDrawing) return;
                    // Mouse hareket ettiğinde yönü belirle tamamen 
                    Yuvarlak.DetermineDirection(e);
                    this.panel1.Invalidate();
                    break;
                case "ücgen":
                    break;
                case "altıgen":
                    break;
                default:
                    break;
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (sayac > 0)
            {
                secim = e.Location;
                Secim(secim);
            }
            switch (sekil)
            {
                case "kare":
                    // Bir elipsing başlangıç koordinatini hatırlamak için...
                    mkare.start = e.Location;
                    mkare.color = MyBrush;
                    if (e.Button == MouseButtons.Left)
                        mDrawing_kare = true;
                    else
                        this.panel1.Invalidate();
                    break;
                case "yuvarlak":
                    // Bir elipsing başlangıç koordinatini hatırlamak için...
                    mEllipse.start = e.Location;
                    mEllipse.color = MyBrush;
                    if (e.Button == MouseButtons.Left)
                        mDrawing = true;
                    else
                        this.panel1.Invalidate();
                    break;
                case "ücgen":
                    break;
                case "altıgen":
                    break;
                default:
                    break;
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (sekil)
            {
                case "kare":
                    if (!mDrawing_kare) return;
                    mDrawing_kare = false;
                    Kare.DetermineDirection_kare(e);
                    // Yarattığımız elipsi liste al
                    mkares.Add(mkare);
                    this.panel1.Invalidate();
                    break;
                case "yuvarlak":
                    if (!mDrawing) return;
                    mDrawing = false;
                    Yuvarlak.DetermineDirection(e);
                    // Yarattığımız elipsi liste al
                    mEllipses.Add(mEllipse);
                    this.panel1.Invalidate();
                    break;
                case "ücgen":
                    break;
                case "altıgen":
                    break;
                default:
                    break;
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ellipse ellipse in mEllipses)
                Yuvarlak.DrawEllipse(ellipse, e);

            if (mDrawing)
                Yuvarlak.DrawEllipse(mEllipse, e);


            foreach (Rectangle kare in mkares)
                Kare.DrawRectangle(kare, e);

            if (mDrawing_kare)
                Kare.DrawRectangle(mkare, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sekil = "kare";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sekil = "yuvarlak";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sekil = "ücgen";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sekil = "altıgen";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Red);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Red);
            sec = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Blue);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Blue);
            sec = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Green);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Green);
            sec = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Orange);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Orange);
            sec = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Black);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Black);
            sec = 0;

        }
        
        private void button10_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Yellow);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Yellow);
            sec = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Purple);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Purple);
            sec = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.Brown);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.Brown);
            sec = 0;
        }
        public SolidBrush GeciciRenk;
        private void button13_Click(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                GeciciRenk = new SolidBrush(Color.White);
                RenkDegis(GeciciRenk);
            }
            else MyBrush = new SolidBrush(Color.White);
            sec = 0;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Metin Dosyası|*.txt";
            mkares.Clear();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
                
                string yazi = sw.ReadLine();
                int sayac = 0;
                while (yazi != null)
                {
                    string[] parcalar = yazi.Split(',');
                    sayac = -1;
                    foreach (string parca in parcalar)
                    {
                        sayac++;
                        if (parcalar[0]=="kare")
                        {
                            switch (sayac)
                            {
                                case 1:
                                    mkare.end.X = Convert.ToInt32(parca);
                                    break;
                                case 2:
                                    mkare.end.Y = Convert.ToInt32(parca);
                                    break;
                                case 3:
                                    mkare.start.X = Convert.ToInt32(parca);
                                    break;
                                case 4:
                                    mkare.start.Y = Convert.ToInt32(parca);
                                    break;
                                case 5:
                                    {
                                        if (parca == "Blue") mkare.color = new SolidBrush(Color.Blue);
                                        else if (parca == "Red") mkare.color = new SolidBrush(Color.Red);
                                        else if (parca == "Green") mkare.color = new SolidBrush(Color.Green);
                                        else if (parca == "Orange") mkare.color = new SolidBrush(Color.Orange);
                                        else if (parca == "Black") mkare.color = new SolidBrush(Color.Black);
                                        else if (parca == "Yellow") mkare.color = new SolidBrush(Color.Yellow);
                                        else if (parca == "Purple") mkare.color = new SolidBrush(Color.Purple);
                                        else if (parca == "Brown") mkare.color = new SolidBrush(Color.Brown);
                                        else if (parca == "White") mkare.color = new SolidBrush(Color.White);
                                        break;
                                    }
                                case 6:
                                    if (parca == "Four") mkare.direction_Kare = Direction_Kare.Four;
                                    if (parca == "Three") mkare.direction_Kare = Direction_Kare.Three;
                                    if (parca == "Two") mkare.direction_Kare = Direction_Kare.Two;
                                    if (parca == "One") mkare.direction_Kare = Direction_Kare.One;
                                    mkares.Add(mkare);
                                    this.panel1.Invalidate();
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (parcalar[0] == "yuvarlak")
                        {
                            switch (sayac)
                            {
                                case 1:
                                    mEllipse.end.X = Convert.ToInt32(parca);
                                    break;
                                case 2:
                                    mEllipse.end.Y = Convert.ToInt32(parca);
                                    break;
                                case 3:
                                    mEllipse.start.X = Convert.ToInt32(parca);
                                    break;
                                case 4:
                                    mEllipse.start.Y = Convert.ToInt32(parca);
                                    break;
                                case 5:
                                    {
                                        if (parca == "Blue") mEllipse.color = new SolidBrush(Color.Blue);
                                        else if (parca == "Red") mEllipse.color = new SolidBrush(Color.Red);
                                        else if (parca == "Green") mEllipse.color = new SolidBrush(Color.Green);
                                        else if (parca == "Orange") mEllipse.color = new SolidBrush(Color.Orange);
                                        else if (parca == "Black") mEllipse.color = new SolidBrush(Color.Black);
                                        else if (parca == "Yellow") mEllipse.color = new SolidBrush(Color.Yellow);
                                        else if (parca == "Purple") mEllipse.color = new SolidBrush(Color.Purple);
                                        else if (parca == "Brown") mEllipse.color = new SolidBrush(Color.Brown);
                                        else if (parca == "White") mEllipse.color = new SolidBrush(Color.White);
                                        break;
                                    }
                                case 6:
                                    if (parca == "Four") mEllipse.direction = Direction.Four;
                                    if (parca == "Three") mEllipse.direction = Direction.Three;
                                    if (parca == "Two") mEllipse.direction = Direction.Two;
                                    if (parca == "One") mEllipse.direction = Direction.One;
                                    mEllipses.Add(mEllipse);
                                    this.panel1.Invalidate();
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    yazi = sw.ReadLine();
                }
                //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
                //Son satır okunduktan sonra okuma işlemini bitirdik
                sw.Close();
                fs.Close();
            }
            this.panel1.Paint += new PaintEventHandler(Panel1_Paint);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Metin Dosyası|*.txt";
            save.OverwritePrompt = true;
            save.CreatePrompt = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Kayit = new StreamWriter(save.FileName);
                foreach (Rectangle kare in mkares)
                    Kayit.WriteLine("kare,"+kare.end.X + "," + kare.end.Y + "," + kare.start.X + "," + kare.start.Y + "," + kare.color.Color.Name+","+kare.direction_Kare);
                foreach (Ellipse ellipse in mEllipses)
                    Kayit.WriteLine("yuvarlak,"+ellipse.end.X + "," + ellipse.end.Y + "," + ellipse.start.X + "," + ellipse.start.Y + "," + ellipse.color.Color.Name + "," + ellipse.direction);
                Kayit.Close();
            }
        }
        int sayac = 0;
        private void button16_Click(object sender, EventArgs e)
        {
            sayac = 0;
            button16.BackColor = Color.Plum;
            sayac++;
            sekil = "";
        }
        private void button17_Click(object sender, EventArgs e)
        {
            sayac = 0;
            button16.BackColor = Color.Transparent;
            foreach (Rectangle kare in mkares)
            {
                if (yedek.start == kare.start && yedek.end == kare.end && yedek.color == kare.color)
                {
                    mkares.Remove(kare);
                    break;
                }
            }
            foreach (Rectangle kare in mkares)
            {
                if (yedek.start.X == kare.start.X - 10 || yedek.start.X == kare.start.X + 10)
                {
                    mkares.Remove(kare);
                    break;
                }
            }
            foreach (Ellipse ellipse in mEllipses)
            {
                if (yedek.start == ellipse.start && yedek.end == ellipse.end && yedek.color == ellipse.color)
                {
                    mEllipses.Remove(ellipse);
                    break;
                }
            }
            foreach (Ellipse ellipse in mEllipses)
            {
                if (yedek.start.X == ellipse.start.X - 10 || yedek.start.X == ellipse.start.X + 10)
                {
                    mEllipses.Remove(ellipse);
                    break;
                }
            }
            sec = 0;
            this.panel1.Invalidate();
            this.panel1.Paint += new PaintEventHandler(Panel1_Paint);
            yedeks.Clear();
        }

        int sec=0;
        public void Secim(Point deger)
        {
            secilen = "";
            sayac = 0;
            foreach (Rectangle kare in mkares)
            {
                if (((Convert.ToInt32(deger.X) >= kare.start.X && Convert.ToInt32(deger.X) <= kare.end.X )||(Convert.ToInt32(deger.X) <= kare.start.X && Convert.ToInt32(deger.X) >= kare.end.X)) &&((Convert.ToInt32(deger.Y) <= kare.start.Y && Convert.ToInt32(deger.Y) >= kare.end.Y) ||(Convert.ToInt32(deger.Y) >= kare.start.Y && Convert.ToInt32(deger.Y) <= kare.end.Y)))
                {
                    yedeks.Clear();
                    secilen = "kare";
                    if (Convert.ToInt32(deger.X) >= kare.start.X)
                    {
                        mkare.start.X = kare.start.X - 10;
                        mkare.end.X = kare.end.X + 10;
                    }
                    else if (Convert.ToInt32(deger.X) <= kare.start.X)
                    {
                        mkare.start.X = kare.start.X + 10;
                        mkare.end.X = kare.end.X - 10;
                    }
                    if (Convert.ToInt32(deger.Y) <= kare.start.Y)
                    {
                        mkare.start.Y = kare.start.Y + 10;
                        mkare.end.Y = kare.end.Y - 10;
                    }
                    else if (Convert.ToInt32(deger.Y) >= kare.start.Y)
                    {
                        mkare.start.Y = kare.start.Y - 10;
                        mkare.end.Y = kare.end.Y + 10;
                    }
                    yedek.start=kare.start;
                    yedek.end = kare.end;
                    yedek.color = kare.color;
                    string x = kare.direction_Kare + "";
                    if (x == "Four") yedek.direction_Yedek = Direction_Yedek.Four;
                    if (x == "Three") yedek.direction_Yedek = Direction_Yedek.Three;
                    if (x == "Two") yedek.direction_Yedek = Direction_Yedek.Two;
                    if (x == "One") yedek.direction_Yedek = Direction_Yedek.One;
                    yedeks.Add(yedek);
                    mkare.color= new SolidBrush(Color.FromArgb(50, 150, 65, 50));
                    mkares.Add(mkare);

                    break;
                }
            }
            if (secilen!="kare")
            {
                //ellipse ise---------------------
                foreach (Ellipse ellipse in mEllipses)
                {
                    if (((Convert.ToInt32(deger.X) >= ellipse.start.X && Convert.ToInt32(deger.X) <= ellipse.end.X) || (Convert.ToInt32(deger.X) <= ellipse.start.X && Convert.ToInt32(deger.X) >= ellipse.end.X)) && ((Convert.ToInt32(deger.Y) <= ellipse.start.Y && Convert.ToInt32(deger.Y) >= ellipse.end.Y) || (Convert.ToInt32(deger.Y) >= ellipse.start.Y && Convert.ToInt32(deger.Y) <= ellipse.end.Y)))
                    {
                        yedeks.Clear();
                        secilen = "yuvarlak";
                        if (Convert.ToInt32(deger.X) >= ellipse.start.X)
                        {
                            mkare.start.X = ellipse.start.X - 10;
                            mkare.end.X = ellipse.end.X + 10;
                        }
                        else if (Convert.ToInt32(deger.X) <= ellipse.start.X)
                        {
                            mkare.start.X = ellipse.start.X + 10;
                            mkare.end.X = ellipse.end.X - 10;
                        }
                        if (Convert.ToInt32(deger.Y) <= ellipse.start.Y)
                        {
                            mkare.start.Y = ellipse.start.Y + 10;
                            mkare.end.Y = ellipse.end.Y - 10;
                        }
                        else if (Convert.ToInt32(deger.Y) >= ellipse.start.Y)
                        {
                            mkare.start.Y = ellipse.start.Y - 10;
                            mkare.end.Y = ellipse.end.Y + 10;
                        }
                        yedek.start = ellipse.start;
                        yedek.end = ellipse.end;
                        yedek.color = ellipse.color;
                        string y = ellipse.direction + "";
                        if (y == "Four") mkare.direction_Kare = Direction_Kare.Four;
                        if (y == "Three") mkare.direction_Kare = Direction_Kare.Three;
                        if (y == "Two") mkare.direction_Kare = Direction_Kare.Two;
                        if (y == "One") mkare.direction_Kare = Direction_Kare.One;
                        yedeks.Add(yedek);
                        mkare.color = new SolidBrush(Color.FromArgb(50, 150, 65, 50));
                        mkares.Add(mkare);
                        break;
                    }
                }
            }
            sec++;
            this.panel1.Invalidate();
            button16.BackColor = Color.Transparent;
            this.panel1.Paint += new PaintEventHandler(Panel1_Paint);
        }
        
        //------------------------------------------
        //Renk Değiştir
        public void RenkDegis(SolidBrush a)
        {
            
            foreach (Yedek yedek in yedeks)
            {
                foreach (Rectangle kare in mkares)
                {
                    if (yedek.start.X == kare.start.X - 10 || yedek.start.X == kare.start.X + 10)
                    {
                        mkares.Remove(kare);
                        break;
                    }
                }

                if (secilen == "kare")
                {
                    foreach (Rectangle kare in mkares)
                    {
                        if (yedek.start == kare.start && yedek.end == kare.end && yedek.start.X == kare.start.X)
                        {
                            mkares.Remove(kare);
                            break;
                        }
                    }
                    mkare.start = yedek.start;
                    mkare.end = yedek.end;
                    string x = yedek.direction_Yedek + "";
                    if (x == "Four") mkare.direction_Kare = Direction_Kare.Four;
                    if (x == "Three") mkare.direction_Kare = Direction_Kare.Three;
                    if (x == "Two") mkare.direction_Kare = Direction_Kare.Two;
                    if (x == "One") mkare.direction_Kare = Direction_Kare.One;
                    mkare.color = a;
                    mkares.Add(mkare);
                    yedeks.Clear();
                    break;
                }
                else if (secilen == "yuvarlak")
                {
                    foreach (Ellipse ellipse in mEllipses)
                    {
                        if (yedek.start == ellipse.start && yedek.end == ellipse.end && yedek.start.X == ellipse.start.X)
                        {
                            mEllipses.Remove(ellipse);
                            break;
                        }
                    }
                    mEllipse.start = yedek.start;
                    mEllipse.end = yedek.end;
                    string x = yedek.direction_Yedek + "";
                    if (x == "Four") mEllipse.direction = Direction.Four;
                    if (x == "Three") mEllipse.direction = Direction.Three;
                    if (x == "Two") mEllipse.direction = Direction.Two;
                    if (x == "One") mEllipse.direction = Direction.One;
                    mEllipse.color = a;
                    mEllipses.Add(mEllipse);
                    yedeks.Clear();
                    break;
                }
            }
            this.panel1.Invalidate();
            this.panel1.Paint += new PaintEventHandler(Panel1_Paint);
        }
        //------------------------------------------
    }
}