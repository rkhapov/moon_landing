using System;

namespace Core.Objects
{
    public static class PhysObjectExtensions
    {
        public static bool IsRectangleObjectsIntersects(this IPhysObject obj, IPhysObject otherObj)
        {
            return IsSegmentIntersects(obj.Cords.X, obj.Size.Width, otherObj.Cords.X, otherObj.Size.Width)
                   && IsSegmentIntersects(obj.Cords.Y, obj.Size.Height, otherObj.Cords.Y, otherObj.Size.Height);
        }

        private static bool IsSegmentIntersects(double firstStart, double firstLength, double secondStart, double secondLength)
        {
            return Math.Min(firstStart + firstLength, secondStart + secondLength) >= Math.Max(firstStart, secondStart);
        }
    }
}