using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Force
{
    public partial class Form1 : Form
    {
        public struct Calle
        {
            public Vector inicio, ancho;
            public float friccion;
            public bool fluido;

            public Calle(Vector i, Vector a, float f = 0, bool fl = false)
            {
                inicio = i;
                ancho = a;
                friccion = f;
                fluido = fl;
            }
        }
        Bitmap btmp, car;
        Graphics fondo;
        List<Auto> autos;
        List<Calle> calles;
        public Form1()
        {
            InitializeComponent();
            car = new Bitmap(@"E:\VS\Force\car.bmp");
            btmp = new Bitmap(picbox.Width, picbox.Height);
            fondo = Graphics.FromImage(btmp);
            picbox.Image = btmp;

            autos = new List<Auto>();
            for(int i = 1; i <= 5;++i)
                autos.Add(new Auto(new Vector(30, 22), new Vector(picbox.Width, picbox.Height),m: i*10));

            calles = new List<Calle>();
            calles.Add(new Calle(new Vector(0, picbox.Height-10), new Vector(picbox.Width, 10), (float)-0.1));
            calles.Add(new Calle(new Vector(200, picbox.Height - 10), new Vector(100, 10), (float)-1.5));
            calles.Add(new Calle(new Vector(300, 0), new Vector(200, picbox.Height), (float)-1, true));
        }

        Vector Transform(Vector v)
        {
            return new Vector(X: v.X, picbox.Height - v.Y);
        }

        private void Click(object sender, MouseEventArgs e)
        {
            if (timer.Enabled) timer.Stop();
            else timer.Start();
        }

        private void tick(object sender, EventArgs e)
        {
            
            fondo.Clear(Color.White);

            //dibuja calles
            DrawStreet(calles[0], Brushes.LightGray);
            DrawStreet(calles[1], Brushes.Brown);
            DrawStreet(calles[2], Brushes.Gray);

            foreach (Auto auto in autos)
            {
                Vector gravedad = new Vector(Y: (float)-0.02);
                gravedad *= auto.Masa;
                auto.AplicarFuerza(gravedad);

                Vector aire = new Vector(X: (float)0.5);
                auto.AplicarFuerza(aire);

                foreach (Calle calle in calles)
                {
                    if (auto.PosAct.X >= calle.inicio.X && auto.PosAct.X <= calle.inicio.X + calle.ancho.X)
                    {
                        Vector friccion = auto.getVelocidad;
                        friccion.setMagnitud(calle.friccion);
                        if (calle.fluido)
                        {
                            float vel = auto.getVelocidad.Magnitud();
                            friccion *= vel * vel;
                        }
                        auto.AplicarFuerza(friccion);
                    }
                }


                auto.accionMover();
                DrawCar(auto);

            }
            picbox.Refresh();
        }

        void DrawCar(Auto a)
        {
            Vector pos = Transform(a.PosAct);
            fondo.DrawImage(car, pos.X-(car.Width/2), pos.Y-(car.Height/2));
        }

        void DrawStreet(Calle c, Brush b)
        {
            fondo.FillRectangle(b, c.inicio.X, c.inicio.Y, c.ancho.X, c.ancho.Y);
        }
    }
}
