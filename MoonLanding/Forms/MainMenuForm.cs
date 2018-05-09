using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Core.Controls;
using Core.Game;
using Core.Objects;
using Core.Physics;
using Core.Tools;
using Size = System.Drawing.Size;

namespace MoonLanding.Forms
{
    public class MainMenuForm : Form
    {
        private readonly Button startButton;
        private readonly Button exitButton;
        
        public MainMenuForm()
        {
            startButton = new Button {Text = "Insert coin"};
            startButton.Click += (s, o) => RunGame();
            Controls.Add(startButton);
            
            exitButton = new Button {Text = "Exit game"};
            exitButton.Click += (s, o) => Close();
            Controls.Add(exitButton);

            SizeChanged += (s, o) =>
            {
                startButton.Location = new Point(0, 0);
                startButton.Size = new Size(ClientSize.Width, ClientSize.Height / 2);
                    
                exitButton.Location = new Point(0, ClientSize.Height / 2);
                exitButton.Size = new Size(ClientSize.Width, ClientSize.Height / 2);
            };

            Load += (s, o) => OnSizeChanged(EventArgs.Empty);
        }

        private void RunGame()
        {
            var game = InitializeGame();
            var gameForm = new GameForm();
            gameForm.SetGame(game);
            gameForm.Closed += (s, o) => Show();
            gameForm.Show();
            Hide();
        }

        private Game InitializeGame()
        {
            var landscape = Landscape.LoadFromImageFile("landscape_test.png",
                color => color.R + color.B + color.G < 100 ? LandscapeCell.Ground : LandscapeCell.Empty);
            var level = Level.Create(landscape, Enumerable.Empty<IPhysObject>(), new MoonPhysics(),
                new Ship(Vector.Create(300, 10), Core.Tools.Size.Create(30, 30), 1, 20));
            return new Game(level, new Controller());
        }
    }
}