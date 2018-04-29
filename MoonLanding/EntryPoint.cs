using System.Linq;
using System.Windows.Forms;
using Core.Game;
using Core.Objects;
using Core.Physics;
using Core.Tools;

namespace MoonLanding
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            var landscape = Landscape.LoadFromImageFile("landscape_test.png",
                color => color.R + color.B + color.G < 100 ? LandscapeCell.Ground : LandscapeCell.Empty);

            var level = Level.Create(landscape, Enumerable.Empty<IPhysObject>(), new EarthPhysics(),
                Ship.Create(100, Core.Tools.Size.Create(10, 10), Vector.Create(450, 50), 1));
            
            var game = new Game(level);
            
            var gameForm = new GameForm();
            gameForm.SetGame(game);
            
            Application.Run(gameForm);
        }
    }
}