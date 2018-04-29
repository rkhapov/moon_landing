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
                UpdateObject(obj, dt);
        }
        
        private static void UpdateObject(IPhysObject obj, double dt)
        {
            obj.Velocity += obj.Acceleration * dt;
            obj.Cords += obj.Velocity * dt;
            
            obj.Update(dt);
        }
    }
}