using System;
using System.Collections.Generic;
using System.Drawing;
using Infrastructure.Tools;
using Size = Infrastructure.Tools.Size;

namespace Infrastructure.Objects
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

        public void Update(double dt)
        {
            //nothing to do here
            //landscape are static
        }

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
            landscape.FillFromText(text);

            return landscape;
        }

        public static Landscape LoadFromImageFile(string path)
        {
            var image = new Bitmap(path);
            
            return LoadFromImage(image);
        }

        public static Landscape LoadFromImage(Bitmap image)
        {
            var landspace = Landscape.Create(Size.Create(image.Width, image.Height));
            landspace.FillFromBitmap(image);

            return landspace;
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
        
        private void FillFromText(List<string> text)
        {
            for (var i = 0; i < Size.Height; i++)
            {
                for (var j = 0; j < Size.Width; j++)
                    landscape[i, j] = text[i][j] == '*' ? GroundCell.Ground : GroundCell.Empty;
            }
        }

        private void FillFromBitmap(Bitmap bitmap)
        {
            for (var i = 0; i < bitmap.Height; i++)
            {
                for (var j = 0; j < bitmap.Width; j++)
                {
                    if (bitmap.GetPixel(j, i) == Color.Black)
                        SetCell(i, j, GroundCell.Ground);
                }
            }
        }
    }
}