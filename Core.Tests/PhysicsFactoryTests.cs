using System;
using Core.Physics;
using FluentAssertions;
using NUnit.Framework;

namespace Core.Tests
{
    [TestFixture]
    public class PhysicsFactoryTests
    {
        [TestCase("meme lord")]
        [TestCase("unknown")]
        [TestCase("")]
        [TestCase("wabba laba dab dab")]
        [TestCase("shit")]
        [TestCase("_moon")]
        public void FromName_InvalidName_ShouldThrowArgumentException(string name)
        {
            Action a = () => { new PhysicsFactory().FromName(name); };
            
            a.Should().Throw<ArgumentException>();
        }

        [TestCase("moon", typeof(MoonPhysics))]
        [TestCase("open space", typeof(OpenSpacePhysics))]
        [TestCase("earth", typeof(EarthPhysics))]
        public void FromName_LowerCasedNames_ShouldReturnExpectedPhysics(string name, Type expected)
        {
            var sut = new PhysicsFactory().FromName(name);

            sut.Should().BeOfType(expected);
        }

        [TestCase("moOn", typeof(MoonPhysics))]
        [TestCase("MOON", typeof(MoonPhysics))]
        [TestCase("mOON", typeof(MoonPhysics))]
        [TestCase("open SPACE", typeof(OpenSpacePhysics))]
        [TestCase("Open sPACE", typeof(OpenSpacePhysics))]
        [TestCase("OpeN sPACE", typeof(OpenSpacePhysics))]
        public void FromName_DifferentLetterCases_ShouldReturnExpectedPhysics(string name, Type expected)
        {
            var sut = new PhysicsFactory().FromName(name);

            sut.Should().BeOfType(expected);
        }
    }
}