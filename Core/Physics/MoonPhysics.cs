using System.Collections.Generic;
using Core.Objects;
using Core.Tools;

namespace Core.Physics
{
    public class MoonPhysics : IPhysics
    {
        private static readonly Vector Gravity = Vector.Create(0, 1.62);

        public string Name => "moon";

        public void Update(IEnumerable<IPhysObject> objectsToUpdate, double dt)
        {
            foreach (var obj in objectsToUpdate)
                UpdateObject(obj, dt);
        }
        
        private static void UpdateObject(IPhysObject obj, double dt)
        {
            var actualAcceleration = obj.Acceleration + Gravity;
            obj.Velocity += actualAcceleration * dt;
            obj.Cords += obj.Velocity * dt;
            
            obj.Update(dt);
        }
    }
}