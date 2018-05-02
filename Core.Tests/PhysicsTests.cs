using System.Collections.Generic;
using Core.Objects;
using Core.Tools;
using NUnit.Framework;
using Size = Core.Tools.Size;

namespace Core.Tests
{
    [TestFixture]
    public class PhysicsTests
    {
        [Test]
        public void OpenSpacePhysics_ShouldntUpdateShipsCoordinates()
        {
//            var ship = Ship.Create(100, new Size(2, 2), Vector.Zero, 5);
            // TODO
        }

        [Test]
        public void MoonPhysics_ShouldUpdateShipsCoordinates()
        {
//            var ship = Ship.Create(100, new Size(2, 2), Vector.Zero, 5);
            // TODO
        }

        [Test]
        public void Physics_ShouldntUpdateLandscapesCoordinates()
        {
            var landscape = Landscape.CreateFromText(new List<string>
            {
                "......................",
                "................******",
                "...............*******",
                "****.........*********",
                "******....************",
                "**********************"
            }, chr => chr == '*' ? LandscapeCell.Ground : LandscapeCell.Empty);
            // TODO
        }
    }
}
