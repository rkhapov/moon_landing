using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Core.Objects;

namespace MoonLanding
{
    public class LandscapeForm : Form
    {
        private readonly Landscape landscape;
        
        public LandscapeForm(Landscape landscape)
        {
            this.landscape = landscape;
            ClientSize = new Size(landscape.Size.Width, landscape.Size.Height);
            DoubleBuffered = true;
            
            var timer = new Timer();
            timer.Interval = 500;
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();

            Paint += (s, args) =>
            {
                for (var i = 0; i < landscape.Size.Height; i++)
                {
                    for (var j = 0; j < landscape.Size.Width; j++)
                    {
                        DrawPixel(args.Graphics, i, j, landscape.GroundAt(i, j) ? Brushes.Black : Brushes.White);
                    }
                }
            };
        }

        private void DrawPixel(Graphics graphics, int i, int j, Brush brush)
        {
            graphics.FillRectangle(brush, j, i, 1, 1);
        }
    }
    
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            var landscape = Landscape.LoadFromImageFile("landscape_test.png", color => color.R + color.G + color.B < 100 ? GroundCell.Ground : GroundCell.Empty);
            
            var landscapeForm = new LandscapeForm(landscape);
            Application.Run(landscapeForm);
        }
    }
}