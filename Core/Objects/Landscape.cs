using System;
using System.Collections.Generic;
using System.Drawing;
using Core.Tools;
using Size = Core.Tools.Size;

namespace Core.Objects
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

        private readonly LandscapeCell[,] landscape;

        private Landscape(Size size)
        {
            Size = size;
            landscape = new LandscapeCell[size.Height, size.Width];

            for (var i = 0; i < size.Height; i++)
            {
                for (var j = 0; j < size.Width; j++)
                    landscape[i, j] = LandscapeCell.Empty;
            }
        }

        public LandscapeCell GetCell(int y, int x)
        {
            return landscape[y, x];
        }
        
        public Landscape SetCell(int y, int x, LandscapeCell value)
        {
            landscape[y, x] = value;
            
            return this;
        }

        public bool GroundAt(int y, int x)
        {
            return GetCell(y, x) == LandscapeCell.Ground;
        }

        public bool InBound(int y, int x)
        {
            return y >= 0 && y < Size.Height && x >= 0 && x < Size.Width;
        }

        public static Landscape Create(Size size)
        {
            return new Landscape(size);
        }

        public static Landscape CreateFromText(List<string> text, Func<char, LandscapeCell> cellPredicate)
        {
            if (text.Count == 0)
                return Create(Size.Zero);
            var landscape = Create(Size.Create(text[0].Length, text.Count));
            landscape.FillFromText(text, cellPredicate);

            return landscape;
        }

        public static Landscape LoadFromImageFile(string path, Func<Color, LandscapeCell> cellPredicate)
        {
            var image = new Bitmap(path);
            
            return LoadFromImage(image, cellPredicate);
        }

        public static Landscape LoadFromImage(Bitmap image, Func<Color, LandscapeCell> cellPredicate)
        {
            var landspace = Landscape.Create(Size.Create(image.Width, image.Height));
            landspace.FillFromBitmap(image, cellPredicate);

            return landspace;
        }

        public bool IntersectsWith(IPhysObject obj)
        {
            if (ReferenceEquals(obj, this))
                return false;

            if (ReferenceEquals(obj, null))
                return false;
            
            if (obj is Landscape)
                throw new ArgumentException("Cant intersect Landscape and Landscape");

            return Intersects(obj);
        }

        private bool Intersects(IPhysObject obj)
        {
            for (var y = (int)Math.Floor(obj.Cords.Y); y < (int)Math.Ceiling(obj.Cords.Y) + obj.Size.Height; y++)
            {
                for (var x = (int) Math.Floor(obj.Cords.X); x < (int) Math.Ceiling(obj.Cords.X) + obj.Size.Width; x++)
                {
                    if (!InBound(y, x))
                        continue;
                    
                    if (GroundAt(y, x))
                        return true;
                }
            }

            return false;
        }
        
        public bool IsLandingSite(int startX,int finishX)
        {
            var y = FindGround(startX);
            if (y == -1) return false;
            for (var x = startX; x <= finishX; x++)
            {
                if (FindGround(x) != y)
                    return false;
            }
            return true;
        }

        private int FindGround(int x)
        {
            const int emptyCount = 2;
            for (var i = emptyCount; i < landscape.GetLength(0); i++)
            {
                var isEmpty = true;
                for (var j = 1; j <= emptyCount; j++)
                    if (landscape[i - j, x] != LandscapeCell.Empty)
                    {
                        isEmpty = false;
                        break;
                    }
                if (isEmpty && landscape[i, x] == LandscapeCell.Ground)
                    return i;
            }
            return -1;
        }
        
        private void FillFromText(List<string> text, Func<char, LandscapeCell> cellPredicate)
        {
            for (var i = 0; i < Size.Height; i++)
            {
                for (var j = 0; j < Size.Width; j++)
                    landscape[i, j] = cellPredicate(text[i][j]);
            }
        }

        private void FillFromBitmap(Bitmap bitmap, Func<Color, LandscapeCell> cellPredicate)
        {
            for (var i = 0; i < bitmap.Height; i++)
            {
                for (var j = 0; j < bitmap.Width; j++)
                    SetCell(i, j, cellPredicate(bitmap.GetPixel(j, i)));
            }
        }
    }
}