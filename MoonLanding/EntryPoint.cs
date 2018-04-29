using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Core.Controls;
using Core.Objects;

namespace MoonLanding
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            Application.Run(new GameForm(new GameController()));
        }
    }
}