using System;
using Core.Objects;
using Core.Tools;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Size = Core.Tools.Size;

namespace Core.Tests
{
    class Obg : IPhysObject
    {
        public Obg(Vector cords, Size size)
        {
            Cords = cords;
            Size = size;
        }

        public Vector Velocity
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Vector Acceleration
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Vector Direction
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Vector Cords { get; set; }
        public Size Size { get; }

        public int Mass
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }


        public bool IntersectsWith(IPhysObject obj)
        {
            return this.IsRectangleObjectsIntersects(obj);
        }

        public void Update(double dt)
        {
            throw new NotImplementedException();
        }
    }

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
            var firstObj = new Obg(cord, size);
            var secondObj = new Obg(cord, size);
            Assert.AreEqual(true, firstObj.IntersectsWith(secondObj));
            Assert.AreEqual(true, secondObj.IntersectsWith(firstObj));
        }

        [Test]
        public void NestingObg_ShouldReturnTrue()
        {
            var firstObj = new Obg(Vector.Create(5, 5), new Size(10, 10));
            var secondObj = new Obg(Vector.Create(1, 1), new Size(20, 20));
            Assert.AreEqual(true, firstObj.IntersectsWith(secondObj));
            Assert.AreEqual(true, secondObj.IntersectsWith(firstObj));
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
            var firstObj = new Obg(firstCord, firstSize);
            var secondObj = new Obg(secondCord, secondSize);
            Assert.AreEqual(true, firstObj.IntersectsWith(secondObj));
            Assert.AreEqual(true, secondObj.IntersectsWith(firstObj));
        }

        [Test]
        [Repeat(100)]
        public void NoIntersectObg_ShouldReturnFalse()
        {
            var firstObj = new Obg(Vector.Create(0, 0), new Size(20, 20));
            var secondObj = new Obg(Vector.Create(Random.Next() + 25, Random.Next() + 25),
                new Size(Random.Next(), Random.Next()));
            Assert.AreEqual(false, firstObj.IntersectsWith(secondObj));
            Assert.AreEqual(false, secondObj.IntersectsWith(firstObj));
        }

        [Test]
        [Repeat(10)]
        public void Empty_ShouldReturnFalse()
        {
            var cord = GetRandomVector();
            var obj = new Obg(cord, new Size(0, 0));
            var otherObj = new Obg(cord, new Size(0, 0));
            Assert.AreEqual(true, obj.IntersectsWith(otherObj));
        }

        [Test]
        [Repeat(10)]
        public void Point_ShouldReturnTrue()
        {
            var cord = GetRandomVector();
            var obj = new Obg(cord, new Size(1, 1));
            var otherObj = new Obg(cord, new Size(1, 1));
            Assert.AreEqual(true, obj.IntersectsWith(otherObj));
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
        public void UpdateKinematicsWithGravity_NonZeroAccelerationButZeroMass_ShouldEnforceKinematicsLawsOnShortTimeSegment()
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
                physObject.Cords.Should().BeEquivalentTo(startCords + startVelocity * t + startAcceleration * t * (t / 2));
                physObject.Velocity.Should().BeEquivalentTo(startVelocity + startAcceleration * t);
                physObject.UpdateKinematicsWithGravity(dt, gravity);
            }
        }
        
        [Test]
        [Repeat(10)]
        public void UpdateKinematicsWithGravity_NonZeroAccelerationAndNonZeroMass_ShouldEnforceKinematicsLawsOnShortTimeSegment()
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