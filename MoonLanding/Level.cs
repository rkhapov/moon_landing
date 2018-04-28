using MoonLanding.Physics;
using System.Collections.Generic;

namespace MoonLanding
{
    public class Level
    {
        public Landscape Landscape { get; }
        public List<IPhysObject> Objects { get; }

        private Level(Landscape landscape, List<IPhysObject> objects)
        {
            Landscape = landscape;
            Objects = objects;
        }

        public static Level Create(Landscape landscape, List<IPhysObject> objects)
        {
            return new Level(landscape, objects);
        }
    }
}
