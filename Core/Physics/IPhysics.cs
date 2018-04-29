using System.Collections.Generic;
using Core.Objects;

namespace Core.Physics
{
    public interface IPhysics
    {
        string Name { get; }
        void Update(IEnumerable<IPhysObject> objectsToUpdate, double dt);
    }
}