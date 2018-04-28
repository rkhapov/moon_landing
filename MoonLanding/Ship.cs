using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonLanding.Physics;
using MoonLanding.Tools;

namespace MoonLanding
{
    class Ship : IPhysObject
    {
        public double Fuel { get; private set; }
        
        public Vector Direction { get; set; }

        public Vector Velocity { get; set; }

        public Vector Acceleration { get; set; }

        public Vector Cords { get; set; }

        public Size Size { get; }

        public int Mass { get; set; }

        private Ship(double fuel, Size size, Vector cords, int mass)
        {
            Velocity = Vector.Zero;
            Acceleration = Vector.Zero;
            Direction = Vector.Create(0, 1);
            Fuel = fuel;
            Size = size;
            Cords = cords;
            Mass = mass;
        }

        public static Ship Create(double fuel, Size size, Vector cords, int mass)
        {
            return new Ship(fuel, size, cords, mass);
        }

        public bool IntersectsWith(IPhysObject obj)
        {
            return Cords == obj.Cords;
        }

        public void ChangeDirection(double angle)
        {
            Direction = Direction.Rotate(angle);
        }

        public void EnableEngine(double dt)
        {
            double fuelConsumption = 1;
            Vector engineAcceleration = Vector.Create(1, 0).Rotate(Direction.Angle); // ???
            Acceleration += engineAcceleration * dt;
            Fuel -= fuelConsumption * dt;
        }

        void IPhysObject.Update(double dt)
        {
            Cords += Velocity * dt;
            Velocity = Acceleration * dt;
        }

        void Die()
        {
            // ??
        }
    }
}
