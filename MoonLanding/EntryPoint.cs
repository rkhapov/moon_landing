using System.Linq;
using System.Media;
using System.Threading;
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
//            var enginePlayer = new SoundPlayer("../resources/engine.wav");
//            enginePlayer.PlayLooping();
//            Thread.Sleep(2000);
//            enginePlayer.Stop();
//            Thread.Sleep(2000);
//            enginePlayer.Play();
//            Thread.Sleep(2000);
        }
    }
}