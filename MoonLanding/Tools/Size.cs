namespace MoonLanding.Tools
{
    public class Size
    {
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        private Size()
        {
            //nothing
        }

        public Size(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public static Size Create(uint width, uint height)
        {
            return new Size() { Width = width, Height = height};
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
    }
}