using System;
using System.Windows.Forms;
using MoonLanding.Forms;

namespace MoonLanding
{
    internal class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new GameForm());
        }
    }
}