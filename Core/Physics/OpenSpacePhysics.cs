using System.Collections.Generic;
using Core.Objects;
using Core.Tools;

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
            obj.UpdateKinematicsWithGravity(dt, Vector.Zero);
            obj.Update(dt);
        }
    }
}