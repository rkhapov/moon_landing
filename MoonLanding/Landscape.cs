using System;
using System.Collections.Generic;
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

        void IPhysObject.Update(double dt)
        {
            return; /* Is it normal? Landscape doesn't change with dt. ((c) Stepan) */
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
        
        public Landscape SetCell(int y, int x, GroundCell value)
        {
            landscape[y, x] = value;
            
            return this;
        }

        public bool GroundAt(int y, int x)
        {
            return GetCell(y, x) == GroundCell.Ground;
        }

        public bool InBound(int y, int x)
        {
            return y >= 0 && y < Size.Height && x >= 0 && x < Size.Width;
        }

        public static Landscape Create(Size size)
        {
            return new Landscape(size);
        }

        public static Landscape CreateFromText(List<string> text)
        {
            if (text.Count == 0)
                return Create(Size.Zero);
            var landscape = Create(Size.Create(text[0].Length, text.Count));
            landscape.FillWithText(text);

            return landscape;
        }

        public static Landscape LoadFromImage(string path)
        {
            return null;
        }

        public bool IntersectsWith(IPhysObject obj)
        {
            if (obj is Landscape)
                throw new ArgumentException("Cant intersect Landscape and Landscape");

            for (var y = (int)Math.Ceiling(obj.Cords.Y); y < (int)Math.Ceiling(obj.Cords.Y) + obj.Size.Height; y++)
            {
                for (var x = (int) Math.Ceiling(obj.Cords.X); x < (int) Math.Ceiling(obj.Cords.X) + obj.Size.Width; x++)
                {
                    if (!InBound(y, x))
                        continue;
                    
                    if (GroundAt(y, x))
                        return true;
                }
            }

            return false;
        }
        
        private void FillWithText(List<string> text)
        {
            for (var i = 0; i < Size.Height; i++)
            {
                for (var j = 0; j < Size.Width; j++)
                    landscape[i, j] = text[i][j] == '*' ? GroundCell.Ground : GroundCell.Empty;
            }
        }
    }
}