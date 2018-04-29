using System.Windows.Forms;
using Core.Controls;

namespace MoonLanding
{
    public class GameForm : Form
    {
        private readonly GameController gameController;
        private readonly Timer gameTimer; 
        
        public GameForm(GameController gameController)
        {
            this.gameController = gameController;
            gameTimer = new Timer() {Interval = 1000};
            gameTimer.Tick += (sender, args) => this.gameController.HandleTick(1);
            gameTimer.Start();
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            gameController.HandleKeyDown(e.KeyCode);
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            gameController.HandleKeyUp(e.KeyCode);
        }
    }
}