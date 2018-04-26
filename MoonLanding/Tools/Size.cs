namespace MoonLanding.Tools
{
    public class Size
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private Size()
        {
            //nothing
        }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static Size Create(int width, int height)
        {
            return new Size() { Width = width, Height = height};
        }

        public static Size Zero { get; } = Create(0, 0);
    }
}