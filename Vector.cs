using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Force
{
    public class Vector
    {
        float x, y;
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public Vector(float X = 0, float Y = 0)
        {
            x = X;
            y = Y;
        }

        public Vector(Vector v)
        {
            x = v.X;
            y = v.Y;
        }

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b)
            => new Vector(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector a, float scalar)
            => new Vector(a.x * scalar, a.y * scalar);
        public static Vector operator /(Vector a, float scalar)
            => new Vector(a.x / scalar, a.y / scalar);
        public override string ToString() => "" + +this.x + " , " + this.y;

        public float Magnitud()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public void Normalizar()
        {
            float temp = Magnitud();
            if(temp != 0)
            {
                x /= temp;
                y /= temp;
            }
        }

        public void setMagnitud(float m)
        {
            Normalizar();
            x *= m;
            y *= m;
        }

        public void Limitar(float lim)
        {
            if (lim < Magnitud()) setMagnitud(lim);
        }
    }
}
