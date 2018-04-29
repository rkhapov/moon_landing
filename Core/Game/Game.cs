using System;
using System.Linq;
using System.Windows.Forms;
using Core.Controls;
using Core.Objects;

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
            if (State != GameState.InProgress) 
                return;
            
            Level.Physics.Update(Level.Objects, dt);
            UpdateGameState();
        }

        private void UpdateGameState()
        {
            var collideObject = GetShipCollidesObject();
            //TODO: make checking for landing and setting game sate
        }

        private IPhysObject GetShipCollidesObject()
        {
            throw new NotImplementedException();
            return Level.Objects.FirstOrDefault(Level.Ship.IntersectsWith);
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