using System;
using Core.Tools;

namespace Core.Objects
{
    public static class PhysObjectExtensions
    {
        public static void UpdateKinematicsWithGravity(this IPhysObject obj, double dt, Vector gravity)
        {
            var actualAcceleration = obj.Acceleration + (obj.Mass == 0 ? Vector.Zero : gravity);
            obj.Velocity += actualAcceleration * dt;
            obj.Cords += obj.Velocity * dt;
        }

        public static bool IsRectangleObjectsIntersects(this IPhysObject obj, IPhysObject otherObj)
        {
            if (ReferenceEquals(obj, otherObj))
                return false;

            if (otherObj == null)
                return false;

            return IsSegmentIntersects(obj.Cords.X, obj.Size.Width, otherObj.Cords.X, otherObj.Size.Width)
                   && IsSegmentIntersects(obj.Cords.Y, obj.Size.Height, otherObj.Cords.Y, otherObj.Size.Height);
        }

        private static bool IsSegmentIntersects(double firstStart, double firstLength, double secondStart, double secondLength)
        {
            return Math.Min(firstStart + firstLength, secondStart + secondLength) >= Math.Max(firstStart, secondStart);
        }

        public static bool IsObjectsIntersectsNew(this IPhysObject obj, IPhysObject otherObj)
        {
            if (ReferenceEquals(obj, otherObj))
                return false;

            if (otherObj == null)
                return false;

            var x1 = obj.Cords.X;
            var y1 = obj.Cords.Y;
            var x2 = x1 + obj.Size.Width * Math.Cos(obj.Direction.Angle) - obj.Size.Height * Math.Sin(obj.Direction.Angle);
            var y2 = y1 + obj.Size.Height * Math.Sin(obj.Direction.Angle) + obj.Size.Width * Math.Cos(obj.Direction.Angle);

            // k = (y1 - y2) / (x1 - x2);
            // b = y2 - k * x2;
            // TODO
            return false;
        }
    }
}