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

        public static Vector CreateZero()
        {
            return Create(0.0, 0.0);
        }

        public static Vector Zero { get; } = Create(0.0, 0.0);

        public static Vector operator +(Vector v1, Vector v2)
        {
            return Vector.Create(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return Create(v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}