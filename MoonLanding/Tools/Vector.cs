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
            return new Vector {X = x, Y = y};
        }
    }
}