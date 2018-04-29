using Core.Tools;

namespace Core.Objects
{
    public interface IPhysObject
    {
        Vector Velocity { get; set; }
        Vector Acceleration { get; set; }
        Vector Cords { get; set; }
        Size Size { get; }
        int Mass { get; set; }

        void Update(double dt);
        bool IntersectsWith(IPhysObject obj);
        //void Serialize(IPhysObject obj); // We will do it later
    }
}