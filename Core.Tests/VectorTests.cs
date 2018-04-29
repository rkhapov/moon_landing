using System;
using Core.Tools;
using FluentAssertions;
using NUnit.Framework;

namespace Core.Tests
{
    [TestFixture]
    public class VectorTests
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
        
        private static Tuple<Vector, Vector> GetRandomVectors()
        {
            return Tuple.Create(GetRandomVector(), GetRandomVector());
        }
        
        [Test]
        [Repeat(100)]
        public void OperatorPlus_ShouldReturnSum()
        {
            var vectors = GetRandomVectors();
            var expected = Vector.Create(vectors.Item1.X + vectors.Item2.X, vectors.Item1.Y + vectors.Item2.Y);

            var sut = vectors.Item1 + vectors.Item2;

            sut.Should().BeEquivalentTo(expected);
        }

        [Test]
        [Repeat(100)]
        public void OperatorMinus_ShouldReturnSubstraction()
        {
            var vectors = GetRandomVectors();
            var expected = Vector.Create(vectors.Item1.X - vectors.Item2.X, vectors.Item1.Y - vectors.Item2.Y);

            var sut = vectors.Item1 - vectors.Item2;

            sut.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        [Repeat(100)]
        public void OperatorScalarMul_ShouldReturnMultiplition()
        {
            var vector = GetRandomVector();
            var scalar = GetRandomDouble();
            var expected = Vector.Create(vector.X * scalar, vector.Y * scalar);
            var expectedLength = vector.Length * Math.Abs(scalar);

            var sut = vector * scalar;
            var sut1 = scalar * vector;

            sut1.Should().BeEquivalentTo(sut);
            sut.Should().BeEquivalentTo(expected);
            sut.Length.Should().BeInRange(expectedLength - 1e-6, expectedLength + 1e-6);
        }
    }
}