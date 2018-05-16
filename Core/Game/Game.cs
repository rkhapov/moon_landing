using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Core.Controls;
using Core.Objects;
using Core.Tools;

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
        public const double MaxVelocityToLand = 35;
        public const double RotationAngle = Math.PI / 20;
        
        public IController Controller { get; private set; }
        public Level Level { get; private set; }
        public GameState State { get; private set; }

        public Game(Level level, IController controller)
        {
            Controller = controller;
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
            var collideObjects = GetShipCollidesObject();

            if (collideObjects.Count != 0)
            {
                if (collideObjects.Count != 1 || collideObjects[0] != Level.Landscape)
                    State = GameState.Failed;
                else
                    SetStateToLanding();
            }
        }

        private List<IPhysObject> GetShipCollidesObject()
        {   
            return Level.Objects
                .Where(Level.Ship.IntersectsWith)
                .ToList();
        }

        private void SetStateToLanding()
        {
            var ship = Level.Ship;

            if (ship.Velocity.Length > MaxVelocityToLand ||
                !Equals(ship.Direction, Ship.NormalDirection))
                State = GameState.Failed;
            else
                State = Level.Landscape.IsObjectLanded(ship) ? GameState.Success : GameState.Failed;
        }

        private void OnKeyDown(Keys key)
        {
            switch (key)
            {
            case Keys.Left:
                Level.Ship.Rotate(-RotationAngle);
                break;
            case Keys.Right:
                Level.Ship.Rotate(+RotationAngle);
                break;
            case Keys.Up:
                Level.Ship.EnableEngine();
                break;
            }
        }

        private void OnKeyUp(Keys key)
        {
            if (key == Keys.Up)
                Level.Ship.DisableEngine();
        }
    }
}