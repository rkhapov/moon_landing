using System;
using Core.Tools;

namespace Core.Objects
{
    public class Ship : IPhysObject
    {
        public double Fuel { get; set; }
        public Vector Direction { get; private set; }
        public Vector Velocity { get; set; }
        public Vector Acceleration { get; set; }
        public Vector Cords { get; set; }
        public Size Size { get; }
        public int Mass { get; set; }
        public bool EngineEnabled { get; private set; }
        public double FuelConsumption { get; private set; }
        public double EnginePower { get; private set; }
        public static Vector NormalDirection => Vector.Create(0, -1);

        public Ship(Vector cords, Size size, double fuelConsumption, double enginePower)
        {
            Cords = cords;
            Size = size;
            Velocity = Vector.Zero;
            Acceleration = Vector.Zero;
            Direction = NormalDirection;
            Fuel = 100;
            Mass = 1;
            EngineEnabled = false;
            FuelConsumption = fuelConsumption;
            EnginePower = enginePower;
        }

        public bool IntersectsWith(IPhysObject obj)
        {
            if (ReferenceEquals(this, obj))
                return false;

            if (ReferenceEquals(obj, null))
                return false;

            if (obj is Landscape)
                return obj.IntersectsWith(this);

            return this.IsRectangleObjectsIntersects(obj);
        }

        public void Rotate(double angle)
        {
            Direction = Direction.Rotate(angle);
        }

        public void EnableEngine()
        {
            if (EngineEnabled)
                return;

            if (FuelIsEmpty())
                return;

            EngineEnabled = true;
            Acceleration = Direction * EnginePower;
        }

        public void DisableEngine()
        {
            Acceleration = Vector.Zero;
            EngineEnabled = false;
        }

        public void Update(double dt)
        {
            if (!EngineEnabled)
                return;


            if (FuelIsEmpty())
            {
                DisableEngine();
                return;
            }

            Fuel -= dt * FuelConsumption;

            Acceleration = Direction * EnginePower;
        }

        private bool FuelIsEmpty()
        {
            return Fuel < 1e-3;
        }
    }
}
