using System;

namespace MoonLanding.Tools
{
    public class Vector
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        private Vector()
        {
            //nothing
        }

        public static Vector Create(double x, double y)
        {
            return new Vector { X = x, Y = y };
        }

<<<<<<< HEAD
        public static Vector CreateZero()
        {
            return Create(0.0, 0.0);
        }

        public static Vector Zero { get; } = Create(0.0, 0.0);

        public static Vector operator +(Vector v1, Vector v2)
=======
        public static Vector operator + (Vector v1, Vector v2)
>>>>>>> a47b86e066a1facda013f2b998124bece08e697f
        {
            return Vector.Create(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator - (Vector v1, Vector v2)
        {
            return Create(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator * (Vector v, double a)
        {
            return Create(v.X * a, v.Y * a);
        }

        public static Vector operator *(double a, Vector v)
        {
            return v * a;
        }

        public static double Length(Vector v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        public static double Angle(Vector v)
        {
            return Math.Atan2(v.X, v.Y);
        }
    }
}