using System.Collections.Generic;

namespace MoonLanding.Physics
{
    public abstract class Physics: IPhysics
    {
        private readonly HashSet<IPhysObject> objects = new HashSet<IPhysObject>();

        public IEnumerable<IPhysObject> Objects => objects;
        public void AddObject(IPhysObject obj)
        {
            objects.Add(obj);
        }

        public abstract void Update(double dt);
    }
}