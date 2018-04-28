using System.Collections.Generic;
using FluentAssertions;
using Infrastructure.Objects;
using Infrastructure.Physics;
using Infrastructure.Tools;
using NUnit.Framework;

namespace Infrastructure.Tests
{
    [TestFixture]
    public class PhysicsTests
    {
        [Test]
        public void OpenSpacePhysics_ShouldntUpdateShipsCoordinates()
        {
            var ship = Ship.Create(100, new Size(2, 2), Vector.Zero, 5);
            // TODO
        }

        [Test]
        public void MoonPhysics_ShouldUpdateShipsCoordinates()
        {
            var ship = Ship.Create(100, new Size(2, 2), Vector.Zero, 5);
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
            });
            // TODO
        }
    }
}
