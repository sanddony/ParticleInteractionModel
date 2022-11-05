using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    public class Vector
    {
        private double x, y;
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator *(Vector v1, double k)
        {
            return new Vector(v1.X * k, v1.Y * k);
        }

        public static Vector operator *(double k, Vector v1)
        {
            return new Vector(v1.X * k, v1.Y * k);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator /(Vector v1, Vector v2)
        {
            return new Vector(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector operator /(Vector v1, double k)
        {
            return new Vector(v1.X / k, v1.Y / k);
        }

        public bool Equal(Vector v2)
        {
            if (x == v2.X && y == v2.Y)
                return true;
            else
                return false;
        }

        public static double FindDistance(Vector v1, Vector v2)
        {
            double Dx = v1.X - v2.X;
            double Dy = v1.Y - v2.Y;
            double d = Math.Sqrt(Dx * Dx + Dy * Dy);
            return d;
        }
        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static void FindTrigonomertyValue(Vector v1,
                                                 Vector v2,
                                                 out double Dx,
                                                 out double Dy,
                                                 out double d,
                                                 out double sin,
                                                 out double cos)
        {
            Dx = v1.X - v2.X;
            Dy = v1.Y - v2.Y;
            d = Math.Sqrt(Dx * Dx + Dy * Dy);
            if (d == 0) d = 0.01;
            sin = Dx / d;
            cos = Dy / d;
        }

    }
}
