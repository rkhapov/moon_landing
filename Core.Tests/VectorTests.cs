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

        [TestCase(3, 4, 5)]
        [TestCase(4, 3, 5)]
        [TestCase(6, 8, 10)]
        [TestCase(8, 6, 10)]
        [TestCase(1, 0, 1)]
        [TestCase(0, -1, 1)]
        [TestCase(0, 0, 0)]
        [TestCase(10, 0, 10)]
        public void Length_ShouldReturnRightValue(double x, double y, double expected)
        {
            var sut = Vector.Create(x, y);

            sut.Length.Should().BeInRange(expected - 1e-6, expected + 1e-6);
        }


        [TestCase(0, 0, 0, Description = "Zero vector have 0 angle")]
        [TestCase(0, 1, Math.PI / 2)]
        [TestCase(-1, 0, Math.PI)]
        [TestCase(0, -1, -Math.PI / 2)]
        [TestCase(-100, 0, Math.PI)]
        [TestCase(1, 1, Math.PI / 4)]
        [TestCase(2, 2, Math.PI / 4)]
        [TestCase(-1, 1, 3 * Math.PI / 4)]
        public void Angle_ShouldReturnRightValue(double x, double y, double expected)
        {
            var sut = Vector.Create(x, y);

            sut.Angle.Should().BeInRange(expected - 1e-3, expected + 1e-3);
        }

        [Test]
        [Repeat(100)]
        public void Norm_ShoudReturnRightValue()
        {
            var vector = GetRandomVector();

            var sut = vector.Norm;

            sut.Length.Should().BeInRange(1 - 1e-6, 1 + 1e-6);
            sut.Angle.Should().BeInRange(vector.Angle - 1e-3, vector.Angle + 1e-3);
        }

        [TestCase(1, 0, Math.PI / 2, 0, 1)]
        [TestCase(1, 0, Math.PI / 4, 0.707106, 0.707106)]
        [TestCase(1, 0, -Math.PI / 4, 0.707106,- 0.707106)]
        [TestCase(1, 0, Math.PI, -1, 0)]
        [TestCase(1, 0, -Math.PI, -1, 0)]
        [TestCase(0.707106, 0.707106, Math.PI/2, -0.707106, 0.707106)]
        public void Rotate_ShouldReturnRightValue(double x1, double y1, double angle, double x, double y)
        {
            var sut = Vector.Create(x1, y1);

            var tt1 = sut.Rotate(angle);

            sut.Length.Should().BeInRange(tt1.Length - 1e-3, tt1.Length + 1e-3);
            tt1.Should().BeEquivalentTo(Vector.Create(x, y));
            tt1.Angle.Should().BeInRange(angle + sut.Angle - 1e-3, angle + sut.Angle + 1e-3);
        }

        [Test]
        [Random(100)]
        public void Rotate_ShouldSafeVectorLength()
        {
            var vector = Vector.Create(0, 1);

            var sut = vector.Rotate(GetRandomDouble());

            sut.Length.Should().BeInRange(vector.Length - 1e-3, vector.Length + 1e-3);
        }
    }
}