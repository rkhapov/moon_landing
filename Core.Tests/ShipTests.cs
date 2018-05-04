using System;
using Core.Objects;
using Core.Tools;
using FluentAssertions;
using NUnit.Framework;

namespace Core.Tests
{
    [TestFixture]
    public class ShipTests
    {
        [Test]
        public void Rotate_ShouldChangeAngle()
        {
            var ship = new Ship(Vector.Zero, Size.Zero, 1, 2);

            ship.Rotate(Math.PI / 4);
            var sut = ship.Direction.Angle;
            sut.Should().BeInRange(-Math.PI / 2 + Math.PI / 4 - 1e-3,-Math.PI / 2 + Math.PI / 4 + 1e-3);

        }
    }
}