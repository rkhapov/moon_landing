using System.Collections.Generic;
using Core.Objects;
using Core.Physics;

namespace Core.Game
{
    public class Level
    {
        public Landscape Landscape { get; set; }
        public IPhysics Physics { get; set; }
        public Ship Ship { get; set; }
        public HashSet<IPhysObject> Objects { get; set; }

        private Level(Landscape landscape, IEnumerable<IPhysObject> objects, IPhysics physics, Ship ship)
        {
            Landscape = landscape;
            Objects = new HashSet<IPhysObject>(objects);
            Physics = physics;
            Ship = ship;
            Objects.Add(Ship);
        }

        public static Level Create(Landscape landscape, IEnumerable<IPhysObject> objects, IPhysics physics, Ship ship)
        {
            return new Level(landscape, objects, physics, ship);
        }
    }
}
