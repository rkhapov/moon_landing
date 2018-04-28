using System.Collections.Generic;
using Infrastructure.Objects;

namespace Infrastructure.Physics
{
    public interface IPhysics
    {
        IEnumerable<IPhysObject> Objects { get; }
        void AddObject(IPhysObject obj);
        void Update(double dt);
    }
}