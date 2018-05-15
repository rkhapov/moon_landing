using System;
using Core.Objects;
using Core.Tools;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Size = Core.Tools.Size;

namespace Core.Tests
{

    [TestFixture]
    class PhysObjectExtensionsTests
    {
        private static readonly Random Random = new Random();

        private static double GetRandomDouble(double min = -100, double max = 100)
        {
            return Random.NextDouble() * (max - min) + min;
        }

        private static Vector GetRandomVector(double min = -100, double max = 100)
        {
            return Vector.Create(GetRandomDouble(min, max), GetRandomDouble(min, max));
        }

        [Test]
        [Repeat(10)]
        public void EqualityObg_ShouldReturnTrue()
        {
            var cord = GetRandomVector();
            var size = new Size(Random.Next(), Random.Next());
            
            var physObj = Substitute.For<IPhysObject>();
            physObj.Cords.Returns(cord);
            physObj.Size.Returns(size);
            physObj.Direction.Returns(Vector.Zero);
            
            var otherPhysObject = Substitute.For<IPhysObject>();
            otherPhysObject.Cords.Returns(cord);
            otherPhysObject.Size.Returns(size);
            otherPhysObject.Direction.Returns(Vector.Zero);
            
            Assert.AreEqual(true, physObj.IsRectangleObjectsIntersects(otherPhysObject));
            Assert.AreEqual(true, otherPhysObject.IsRectangleObjectsIntersects(physObj));
        }

        [Test]
        public void NestingObg_ShouldReturnTrue()
        {
            var physObj = Substitute.For<IPhysObject>();
            physObj.Cords.Returns(Vector.Create(5, 5));
            physObj.Size.Returns(new Size(10, 10));
            physObj.Direction.Returns(Vector.Zero);
            
            var otherPhysObject = Substitute.For<IPhysObject>();
            otherPhysObject.Cords.Returns(Vector.Create(1, 1));
            otherPhysObject.Size.Returns(new Size(20, 20));
            otherPhysObject.Direction.Returns(Vector.Zero);
            
            Assert.AreEqual(true, physObj.IsRectangleObjectsIntersects(otherPhysObject));
            Assert.AreEqual(true, otherPhysObject.IsRectangleObjectsIntersects(physObj));
        }

        [Test]
        public void IntersectObg_ShouldReturnTrue()
        {
            DoIntersectObg(Vector.Create(0, 0), new Size(20, 20), Vector.Create(20, 20), new Size(20, 20));
            DoIntersectObg(Vector.Create(0, 0), new Size(20, 20), Vector.Create(20, 0), new Size(20, 20));
            DoIntersectObg(Vector.Create(0, 0), new Size(20, 20), Vector.Create(20, 5), new Size(10, 10));
            DoIntersectObg(Vector.Create(0, 0), new Size(20, 20), Vector.Create(0, 20), new Size(20, 20));
            DoIntersectObg(Vector.Create(0, 0), new Size(20, 20), Vector.Create(5, 20), new Size(10, 10));
        }

        private void DoIntersectObg(Vector firstCord, Size firstSize, Vector secondCord, Size secondSize)
        {
            var physObj = Substitute.For<IPhysObject>();
            physObj.Cords.Returns(firstCord);
            physObj.Size.Returns(firstSize);
            physObj.Direction.Returns(Vector.Zero);
            
            var otherPhysObject = Substitute.For<IPhysObject>();
            otherPhysObject.Cords.Returns(secondCord);
            otherPhysObject.Size.Returns(secondSize);
            otherPhysObject.Direction.Returns(Vector.Zero); 
            
            Assert.AreEqual(true, physObj.IsRectangleObjectsIntersects(otherPhysObject));
            Assert.AreEqual(true, otherPhysObject.IsRectangleObjectsIntersects(physObj));
        }

        [Test]
        [Repeat(100)]
        public void NoIntersectObg_ShouldReturnFalse()
        {
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords.Returns(Vector.Create(0, 0));
            physObject.Size.Returns(new Size(20, 20));
            physObject.Direction.Returns(Vector.Zero);
            
            var otherPhysObject = Substitute.For<IPhysObject>();
            otherPhysObject.Cords.Returns(Vector.Create(Random.Next() + 25, Random.Next() + 25));
            otherPhysObject.Size.Returns(new Size(Random.Next(), Random.Next()));
            otherPhysObject.Direction.Returns(Vector.Zero);
            
            Assert.AreEqual(false, physObject.IsRectangleObjectsIntersects(otherPhysObject));
            Assert.AreEqual(false, otherPhysObject.IsRectangleObjectsIntersects(physObject));
        }

        [Test]
        [Repeat(10)]
        public void Empty_ShouldReturnTrue()
        {
            var cord = GetRandomVector();
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords.Returns(cord);
            physObject.Size.Returns(new Size(0, 0));
            physObject.Direction.Returns(Vector.Zero);
            
            var otherPhysObject = Substitute.For<IPhysObject>();
            otherPhysObject.Cords.Returns(cord);
            otherPhysObject.Size.Returns(new Size(0, 0));
            otherPhysObject.Direction.Returns(Vector.Zero);

            Assert.AreEqual(true, physObject.IsRectangleObjectsIntersects(otherPhysObject));
        }

        [Test]
        [Repeat(10)]
        public void Point_ShouldReturnTrue()
        {
            var cord = GetRandomVector();
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords.Returns(cord);
            physObject.Size.Returns(new Size(1, 1));
            physObject.Direction.Returns(Vector.Zero);
            
            var otherPhysObject = Substitute.For<IPhysObject>();
            otherPhysObject.Cords.Returns(cord);
            otherPhysObject.Size.Returns(new Size(1, 1));
            otherPhysObject.Direction.Returns(Vector.Zero);

            Assert.AreEqual(true,physObject.IsRectangleObjectsIntersects(otherPhysObject));
        }


        [Test]
        [Repeat(10)]
        public void UpdateKinematicsWithGravity_WithoutAcceleration_ShouldEnforceNewtonFirstLaw()
        {
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords = GetRandomVector();
            physObject.Velocity = GetRandomVector();
            physObject.Acceleration = Vector.Zero;
            var expectedCords = physObject.Cords;
            var expectedVelocity = physObject.Velocity;
            var expectedAcceleration = physObject.Acceleration;

            var dt = 0.05;
            for (var t = 0.0; t < 10; t += dt)
            {
                physObject.Cords.Should().BeEquivalentTo(expectedCords + expectedVelocity * t);
                /*physObject.Velocity.Should().BeEquivalentTo(expectedVelocity);
                physObject.Acceleration.Should().BeEquivalentTo(expectedAcceleration);*/

                physObject.UpdateKinematicsWithGravity(dt, Vector.Zero);
                //expectedCords += expectedVelocity * dt;
            }
        }

        [Test]
        [Repeat(10)]
        public void UpdateKinematicsWithGravity_NonZeroGravityButZeroMass_ShouldEnforceNewtonFirstLaw()
        {
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords = GetRandomVector();
            physObject.Velocity = GetRandomVector();
            physObject.Acceleration = Vector.Zero;
            physObject.Mass = 0;
            var startCords = physObject.Cords;
            var startVelocity = physObject.Velocity;
            var gravity = GetRandomVector();
            var dt = 0.05;

            for (var t = 0.0; t < 10; t += dt)
            {
                physObject.Cords.Should().BeEquivalentTo(startCords + startVelocity * t);
                physObject.UpdateKinematicsWithGravity(dt, gravity);
            }
        }

        [Test]
        [Repeat(10)]
        public void
            UpdateKinematicsWithGravity_NonZeroAccelerationButZeroMass_ShouldEnforceKinematicsLawsOnShortTimeSegment()
        {
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords = GetRandomVector();
            physObject.Velocity = GetRandomVector();
            physObject.Acceleration = GetRandomVector();
            physObject.Mass = 0;
            var startCords = physObject.Cords;
            var startVelocity = physObject.Velocity;
            var startAcceleration = physObject.Acceleration;
            var gravity = GetRandomVector();
            var dt = 0.00001;

            for (var t = 0.0; t < 0.005; t += dt)
            {
                physObject.Cords.Should()
                    .BeEquivalentTo(startCords + startVelocity * t + startAcceleration * t * (t / 2));
                physObject.Velocity.Should().BeEquivalentTo(startVelocity + startAcceleration * t);
                physObject.UpdateKinematicsWithGravity(dt, gravity);
            }
        }

        [Test]
        [Repeat(10)]
        public void
            UpdateKinematicsWithGravity_NonZeroAccelerationAndNonZeroMass_ShouldEnforceKinematicsLawsOnShortTimeSegment()
        {
            var physObject = Substitute.For<IPhysObject>();
            physObject.Cords = GetRandomVector();
            physObject.Velocity = GetRandomVector();
            physObject.Acceleration = GetRandomVector();
            physObject.Mass = 1;
            var startCords = physObject.Cords;
            var startVelocity = physObject.Velocity;
            var startAcceleration = physObject.Acceleration;
            var gravity = GetRandomVector();
            var dt = 0.00001;

            for (var t = 0.0; t < 0.005; t += dt)
            {
                physObject.Cords.Should()
                    .BeEquivalentTo(startCords + startVelocity * t + (startAcceleration + gravity) * t * (t / 2));

                physObject.Velocity.Should().BeEquivalentTo(startVelocity + (startAcceleration + gravity) * t);

                physObject.UpdateKinematicsWithGravity(dt, gravity);
            }
        }
    }
}