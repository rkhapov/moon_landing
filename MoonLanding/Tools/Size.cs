using System;

namespace MoonLanding.Tools
{
    public class Size
    {
        public int Width { get; }
        public int Height { get; }

        private Size()
        {
            //nothing
        }

        public Size(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("Size cant be negative");
            
            Width = width;
            Height = height;
        }

        public static Size Create(int width, int height)
        {
            return new Size(width, height);
        }

        public static Size Zero { get; } = Create(0, 0);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            
            if (ReferenceEquals(obj, this))
                return true;

            if (!(obj is Size))
                return false;

            return Equals((Size)obj);
        }

        protected bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public override int GetHashCode()
        {
            return (Width.GetHashCode() * 1037) ^ Height.GetHashCode();
        }
    }
}