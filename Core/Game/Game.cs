using System;
using System.Windows.Forms;
using Core.Controls;

namespace Core.Game
{
    public enum GameState
    {
        InProgress = 1,
        Failed,
        Success
    }
    
    public class Game
    {
        public Controller Controller { get; private set; }
        public Level Level { get; private set; }
        public GameState State { get; private set; }

        public Game(Level level)
        {
            Controller = new Controller();
            Controller.Tick += OnWorldUpdate;
            Controller.KeyDown += OnKeyDown;
            Controller.KeyUp += OnKeyUp;

            State = GameState.InProgress;
            Level = level;
        }

        private void OnWorldUpdate(double dt)
        {
            Level.Physics.Update(Level.Objects, dt);
        }

        private void OnKeyDown(Keys key)
        {
            switch (key)
            {
            case Keys.Left:
                Level.Ship.ChangeDirection(0.05);
                break;
            case Keys.Right:
                Level.Ship.ChangeDirection(-0.05);
                break;
            case Keys.Up:
//                Level.Ship.EnableEngine();
                break;
            }
        }

        private void OnKeyUp(Keys key)
        {
            /*if (key == Keys.Up)
                Level.Ship.DisableEngine();*/
        }
    }
}