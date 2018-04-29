using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Physics
{
    public class PhysicsFactory : IPhysicsFactory
    {
        private readonly Dictionary<string, IPhysics> physics;

        public PhysicsFactory()
        {
            physics = Assembly
                .GetAssembly(GetType())
                .GetTypes()
                .Where(type => typeof(IPhysics).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IPhysics>()
                .ToDictionary(x => x.Name, x => x, StringComparer.OrdinalIgnoreCase);
        }
        
        public IPhysics FromName(string name)
        {
            if (!physics.TryGetValue(name, out var physic))
                throw new ArgumentException($"Unknown physics name {name}");

            return physic;
        }
    }
}