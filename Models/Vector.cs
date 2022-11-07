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

        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static double FindDistances(Vector point1, Vector point2)
        {
            double Dx = FindDx(point1, point2);
            double Dy = FindDy(point1, point2);
            return Math.Sqrt(Dx * Dx + Dy * Dy);
        }

        public static double FindDx(Vector point1, Vector point2)
        {
            return point1.X - point2.X; 
        }

        public static double FindDy(Vector point1, Vector point2)
        {
            return point1.Y - point2.Y;
        }

        public static void FindSinCos(Vector v1,
                                      Vector v2,
                                      out double sin,
                                      out double cos)
        {
            double d = FindDistances(v1, v2);
            if (d == 0) d = 0.01;
            sin = FindDx(v1,v2) / d;
            cos = FindDy(v1,v2) / d;
        }

    }
}
