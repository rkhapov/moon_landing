using System;
using MoonLanding.Physics;
using MoonLanding.Tools;

namespace MoonLanding
{
    public class Landscape : IPhysObject
    {
        public Vector Velocity
        {
            get => Vector.Zero;
            set { /*cant set velocity of landscape*/ }
        }

        public Vector Acceleration 
        {
            get => Vector.Zero;
            set { /*cant set acceleration of landscape*/ }
        }
        
        public Vector Cords 
        {
            get => Vector.Zero;
            set { /*cant set cords of landscape*/ }
        }
        
        public int Mass 
        {
            get => 0;
            set { /*cant set mass of landscape*/ }
        }
        
        public Size Size { get; set; }


        public static Landscape Create(Size size)
        {
            return null;
        }

        public static Landscape LoadFromImage(string path)
        {
            return null;
        }

        public bool IntersectsWith(IPhysObject obj)
        {
            if (obj is Landscape)
                throw new ArgumentException("Cant intersect Landscape and Landscape");

            return false;
        }
    }
}