using System.Collections.Generic;

namespace MoonLanding.Physics
{
    public interface IPhysics
    {
        IEnumerable<IPhysObject> Objects { get; }
        void AddObject(IPhysObject obj);
        void Update(double dt);
    }
}