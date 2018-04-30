using System.Linq;
using System.Windows.Forms;
using Core.Game;
using Core.Objects;
using Core.Physics;
using Core.Tools;
using MoonLanding.Forms;

namespace MoonLanding
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            Application.Run(new MainMenuForm());
        }
    }
}