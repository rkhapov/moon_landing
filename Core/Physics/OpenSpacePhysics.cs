using System.Collections.Generic;
using Core.Objects;

namespace Core.Physics
{
    public class OpenSpacePhysics : IPhysics
    {
        public string Name => "open space";

        public void Update(IEnumerable<IPhysObject> objectsToUpdate, double dt)
        {
            foreach (var obj in objectsToUpdate)
                obj.Update(dt);
        }
    }
}