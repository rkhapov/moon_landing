using MoonLanding.Tools;

namespace MoonLanding.Physics
{
    public interface IPhysObject
    {
        Vector Velocity { get; set; }
        Vector Acceleration { get; set; }
        Vector Cords { get; set; }
        Size Size { get; }
        int Mass { get; set; }

        bool IntersectsWith(IPhysObject obj);
    }
}