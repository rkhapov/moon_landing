﻿using FluentAssertions;
using MoonLanding.Tools;
using NUnit.Framework;

namespace MoonLanding.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void OperatorPlus_AddingTwoVectors_ShouldReturnSum()
        {
            var v1 = Vector.Create(5, 6);
            var v2 = Vector.Create(-3, 10);

            var sum = v1 + v2;

            sum.X.Should().BeInRange(2 - 1e-6, 2 + 1e-6);
            sum.Y.Should().BeInRange(16 - 1e-6, 16 + 1e-6);
        }
    }
}