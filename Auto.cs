using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Force
{
    public class Auto
    {
        Vector posAct, velocidad, aceleracion, limMap;
        float masa;

        public float Masa { get => masa; set => masa = value; }
        public Vector PosAct { get => posAct; }
        public Vector getVelocidad { get => new Vector(velocidad.X, velocidad.Y); }

        public Auto(Vector pos, Vector limMap, float m = 2)
        {
            posAct = pos;
            velocidad = new Vector();
            aceleracion = new Vector();
            this.limMap = limMap;
            masa = m;
        }

        public void accionMover()
        {
            posAct += velocidad;
            velocidad += aceleracion;
            aceleracion *= 0;
            Limites(true);
        }
        
        void Limites(bool cage)
        {
            if (cage)
            {
                if (posAct.X > limMap.X) posAct.X = limMap.X;
                else if (posAct.X < 0) posAct.X = 0;
                if (posAct.Y > limMap.Y) posAct.Y = limMap.Y;
                else if (posAct.Y < 22) posAct.Y = 22;
                return;
            }
            if (posAct.X >= limMap.X) posAct.X = 0;
            else if (posAct.X < 0) posAct.X = limMap.X; 
            if (posAct.Y >= limMap.Y) posAct.Y = 0;
            else if (posAct.Y < 0) posAct.Y = limMap.Y;
        }

        public void AplicarFuerza(Vector fuerza)
        {
            aceleracion += fuerza/masa;
        }

    }
}
