using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentAssertions;
using MoonLanding.Tools;
using NUnit.Framework;

namespace MoonLanding.Tests
{
    [TestFixture]
    public class LandscapeTests
    {
        private static IEnumerable<TestCaseData> CreateSizeTestsSources()
        {
            yield return new TestCaseData(Size.Create(5, 5));
            yield return new TestCaseData(Size.Create(0, 0));
            yield return new TestCaseData(Size.Create(0, 1));
            yield return new TestCaseData(Size.Create(10, 10));
            yield return new TestCaseData(Size.Create(4, 100));
            yield return new TestCaseData(Size.Create(5000, 5));
            yield return new TestCaseData(Size.Create(100, 100));
        }

        [TestCaseSource(nameof(CreateSizeTestsSources))]
        public void Create_NormalSize_ShouldReturnEmptyFieldWithRightSize(Size size)
        {
            var sut = Landscape.Create(size);

            sut.Size.Should().BeEquivalentTo(size);
            for (var i = 0; i < size.Height; i++)
            {
                for (var j = 0; j < size.Width; j++)
                    sut.GetCell(i, j).Should().BeEquivalentTo(GroundCell.Empty);
            }
        }

        [Test]
        public void Velocity_SettingValue_ShouldStayZero()
        {
            var landscape = Landscape.Create(Size.Create(10, 10));

            landscape.Cords = Vector.Create(5, 6);
            
            landscape.Cords.Should().BeEquivalentTo(Vector.Zero);
        }
        
        [Test]
        public void Cords_SettingValue_ShouldStayZero()
        {
            var landscape = Landscape.Create(Size.Create(10, 10));

            landscape.Cords = Vector.Create(5, 6);
            
            landscape.Cords.Should().BeEquivalentTo(Vector.Zero);
        }
        
        [Test]
        public void Acceleration_SettingValue_ShouldStayZero()
        {
            var landscape = Landscape.Create(Size.Create(10, 10));

            landscape.Acceleration = Vector.Create(5, 6);
            
            landscape.Acceleration.Should().BeEquivalentTo(Vector.Zero);
        }
        
        [Test]
        public void Mass_SettingValue_ShouldStayZero()
        {
            var landscape = Landscape.Create(Size.Create(10, 10));

            landscape.Mass = 42;
            
            landscape.Mass.Should().Be(0);
        }

        [Test]
        public void GetCellSetCell_WorkingWithValues_ShouldReturnRightValues()
        {
            var random = new Random();
            var landscape = Landscape.Create(Size.Create((uint)random.Next(10, 100), (uint)random.Next(10, 100)));
            var exceptedLandscape = new GroundCell[landscape.Size.Height, landscape.Size.Width];

            for (var i = 0; i < landscape.Size.Height; i++)
            {
                for (var j = 0; j < landscape.Size.Width; j++)
                {
                    exceptedLandscape[i, j] = random.Next(1, 2) == 1 ? GroundCell.Empty : GroundCell.Ground;
                    landscape.SetCell(i, j, exceptedLandscape[i, j]);
                }
            }
            
            
            for (var i = 0; i < landscape.Size.Height; i++)
            {
                for (var j = 0; j < landscape.Size.Width; j++)
                {
                    landscape.GetCell(i, j).Should().BeEquivalentTo(exceptedLandscape[i, j]);
                }
            }
        }
    }
}