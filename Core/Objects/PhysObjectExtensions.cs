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

        private static bool IsRectangleObjectsIntersectsold(this IPhysObject obj, IPhysObject otherObj)
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

        public static bool IsRectangleObjectsIntersects(this IPhysObject obj, IPhysObject otherObj)
        {
            if (ReferenceEquals(obj, otherObj))
                return false;

            if (otherObj == null)
                return false;

            if (!IsRectangleObjectsIntersectsold(obj, otherObj))
                return false;

            var x11 = obj.Cords.X;
            var y11 = obj.Cords.Y;

            var x12 = x11 + obj.Size.Width * Math.Cos(obj.Direction.Angle) - obj.Size.Height * Math.Sin(obj.Direction.Angle);
            var y12 = y11 + obj.Size.Width * Math.Sin(obj.Direction.Angle) + obj.Size.Height * Math.Cos(obj.Direction.Angle);

            var x13 = x11 + obj.Size.Width * Math.Cos(obj.Direction.Angle);
            var y13 = y11 + obj.Size.Width * Math.Sin(obj.Direction.Angle);

            var x14 = x11 - obj.Size.Height * Math.Sin(obj.Direction.Angle);
            var y14 = y11 + obj.Size.Height * Math.Cos(obj.Direction.Angle);

            var x21 = otherObj.Cords.X;
            var y21 = otherObj.Cords.Y;

            var x22 = x21 + otherObj.Size.Width * Math.Cos(otherObj.Direction.Angle) - otherObj.Size.Height * Math.Sin(otherObj.Direction.Angle);
            var y22 = y21 + otherObj.Size.Width * Math.Sin(otherObj.Direction.Angle) + otherObj.Size.Height * Math.Cos(otherObj.Direction.Angle);

            var x23 = x21 + otherObj.Size.Width * Math.Cos(otherObj.Direction.Angle);
            var y23 = y21 + otherObj.Size.Width * Math.Sin(otherObj.Direction.Angle);

            var x24 = x21 - otherObj.Size.Height * Math.Sin(otherObj.Direction.Angle);
            var y24 = y21 + otherObj.Size.Height * Math.Cos(otherObj.Direction.Angle);

            var rectangle1 = new[] { Tuple.Create(x11, y11, x13, y13), Tuple.Create(x11, y11, x14, y14), Tuple.Create(x12, y12, x13, y13), Tuple.Create(x12, y12, x14, y14) };
            var rectangle2 = new[] { Tuple.Create(x21, y21, x23, y23), Tuple.Create(x21, y21, x24, y24), Tuple.Create(x22, y22, x23, y23), Tuple.Create(x22, y22, x24, y24) };

            foreach (var storona1 in rectangle1)
                foreach (var storona2 in rectangle2)
                    if (AreLinesIntersect(storona1.Item1, storona1.Item2, storona1.Item3, storona1.Item4, storona2.Item1, storona2.Item2, storona2.Item3, storona2.Item4))
                        return true;

            return false;
        }

        private static bool AreLinesIntersect(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            var A1 = y1 - y2;
            var B1 = x2 - x1;
            var C1 = x1 * y2 - x2 * y1;

            var A2 = y3 - y4;
            var B2 = x4 - x3;
            var C2 = x3 * y4 - x4 * y3;

            if (A1 * B2 - A2 * B1 < 1e-6)
            {
                if (C1 - C2 < 1e-6)
                    return (x3 <= Math.Max(x1, x2) && x3 >= Math.Min(x1, x2)) || (x4 <= Math.Max(x1, x2) && x4 >= Math.Min(x1, x2))
                        || (y3 <= Math.Max(y1, y2) && y3 >= Math.Min(y1, y2)) || (y4 <= Math.Max(y1, y2) && y4 >= Math.Min(y1, y2))
                        || (x1 <= Math.Max(x3, x4) && x1 >= Math.Min(x3, x4)) || (x2 <= Math.Max(x3, x4) && x2 >= Math.Min(x3, x4))
                        || (y1 <= Math.Max(y3, y4) && y1 >= Math.Min(y3, y4)) || (y2 <= Math.Max(y3, y4) && y2 >= Math.Min(y3, y4));
                else
                    return IsSegmentIntersects(x1, Math.Abs(x1 - x2), x3, Math.Abs(x3 - x4)) && IsSegmentIntersects(y1, Math.Abs(y1 - y2), y3, Math.Abs(y3 - y4));
            }
            else
            {
                var x = -(C1 * B2 - C2 * B1) / (A1 * B2 - A2 * B1);
                var y = -(A1 * C2 - A2 * C1) / (A1 * B2 - A2 * B1);
                return (x <= Math.Max(x1, x2) && x >= Math.Min(x1, x2) && y <= Math.Max(y1, y2) && y >= Math.Min(y1, y2));
            }
        }
    }
}