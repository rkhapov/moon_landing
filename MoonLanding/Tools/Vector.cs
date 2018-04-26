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

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector { X = v1.X + v2.X, Y = v1.Y + v2.Y };
        }
    }
}