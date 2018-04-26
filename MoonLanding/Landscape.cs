using System;
using MoonLanding.Physics;
using MoonLanding.Tools;

namespace MoonLanding
{
    public class Landscape : IPhysObject
    {
        public Vector Velocity
        {
            get => Vector.Zero;
            set { /*cant set velocity of landscape*/ }
        }

        public Vector Acceleration 
        {
            get => Vector.Zero;
            set { /*cant set acceleration of landscape*/ }
        }
        
        public Vector Cords 
        {
            get => Vector.Zero;
            set { /*cant set cords of landscape*/ }
        }
        
        public int Mass 
        {
            get => 0;
            set { /*cant set mass of landscape*/ }
        }
        
        public Size Size { get; }

        private readonly GroundCell[,] landscape;

        private Landscape(Size size)
        {
            Size = size;
            landscape = new GroundCell[size.Height, size.Width];

            for (var i = 0; i < size.Height; i++)
            {
                for (var j = 0; j < size.Width; j++)
                    landscape[i, j] = GroundCell.Empty;
            }
        }

        public GroundCell GetCell(int y, int x)
        {
            return landscape[y, x];
        }

        public void SetCell(int y, int x, GroundCell value)
        {
            landscape[y, x] = value;
        }

        public static Landscape Create(Size size)
        {
            return new Landscape(size);
        }

        public static Landscape LoadFromImage(string path)
        {
            return null;
        }

        public bool IntersectsWith(IPhysObject obj)
        {
            if (obj is Landscape)
                throw new ArgumentException("Cant intersect Landscape and Landscape");

            return false;
        }
    }
}