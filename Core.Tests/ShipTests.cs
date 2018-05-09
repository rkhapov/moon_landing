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
        private static Ship CreateShip()
        {
            return new Ship(Vector.Zero, Size.Create(10, 10), 1, 10);
        }
        
        [Test]
        public void Rotate_ShouldChangeDirectionRight()
        {
            var ship = CreateShip();
            var originalDirection = ship.Direction;

            ship.Rotate(Math.PI / 2);
            
            ship.Direction.Should().BeEquivalentTo(originalDirection.Rotate(Math.PI / 2));
        }

        [Test]
        public void EnableEngine_NonZeroFuel_ShouldEnableEngine()
        {
            var ship = CreateShip();
            
            ship.EnableEngine();

            ship.EngineEnabled.Should().BeTrue();
        }

        [Test]
        public void EnableEngine_EmptyFuel_ShouldntEnableEngine()
        {
            var ship = CreateShip();
            ship.Fuel = 0;
            
            ship.EnableEngine();
        }

        [Test]
        public void DisableEngine_WithEnabledEngine_ShouldDisableEngine()
        {
            var ship = CreateShip();
            ship.EnableEngine();
            
            ship.DisableEngine();

            ship.EngineEnabled.Should().BeFalse();
        }

        [Test]
        public void DisableEngine_WithDisabledEngine_ShouldKeepEngineDisabled()
        {
            var ship = CreateShip();
            
            ship.DisableEngine();

            ship.EngineEnabled.Should().BeFalse();
        }

        [Test]
        public void Acceleration_WithDisabledEngine_ShouldBeZero()
        {
            var ship = CreateShip();
            
            ship.Acceleration.Should().BeEquivalentTo(Vector.Zero);
        }

        [Test]
        public void Acceleration_WithEnabledWngine_ShouldBeRight()
        {
            var ship = CreateShip();
            
            ship.EnableEngine();
            
            ship.Acceleration.Should().BeEquivalentTo(ship.Direction * ship.EnginePower);
        }

        [Test]
        public void Update_WithDisabledEngine_ShouldSafeFuel()
        {
            var ship = CreateShip();
            var oldFuel = ship.Fuel;
            
            ship.Update(1);

            ship.Fuel.Should().BeInRange(oldFuel - 1e-6, oldFuel + 1e-6);
        }

        [Test]
        public void Update_WithEnabledEngines_ShouldDecreaseFueldRight()
        {
            var ship = CreateShip();
            ship.EnableEngine();
            var expectedFuel = ship.Fuel - ship.FuelConsumption * 1;

            ship.Update(1);

            ship.Fuel.Should().BeInRange(expectedFuel - 1e-6, expectedFuel + 1e-6);
        }

        [Test]
        public void Update_WithEmptyEngine_ShouldDisableEngine()
        {
            var ship = CreateShip();
            ship.EnableEngine();
            ship.Fuel = 0;

            ship.Update(1);

            ship.EngineEnabled.Should().BeFalse();
            ship.Fuel.Should().BeInRange(0 - 1e-6, 0 + 1e-6);
        }
    }
}