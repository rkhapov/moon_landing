using System;

namespace MoonLanding.Tools
{
    public class Vector
    {
        public double X { get; }
        public double Y { get; }

        private Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector Create(double x, double y)
        {
            return new Vector(x, y);
        }

        public static Vector CreateZero()
        {
            return Create(0.0, 0.0);
        }

        public static Vector Zero { get; } = Create(0.0, 0.0);
        public double Length => Math.Sqrt(X * X + Y * Y);
        public double Angle => Math.Atan2(X, Y);

        public static Vector operator + (Vector v1, Vector v2)
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            
            if (ReferenceEquals(obj, this))
                return true;

            if (!(obj is Vector))
                return false;

            return Equals((Vector)obj);
        }

        protected bool Equals(Vector other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}