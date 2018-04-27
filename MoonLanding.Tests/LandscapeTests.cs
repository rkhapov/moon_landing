using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentAssertions;
using MoonLanding.Physics;
using MoonLanding.Tools;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.Exceptions;

namespace MoonLanding.Tests
{
    [TestFixture]
    public class LandscapeTests
    {
        private static IEnumerable<TestCaseData> CreateSizeTestCases()
        {
            yield return new TestCaseData(Size.Create(5, 5));
            yield return new TestCaseData(Size.Create(0, 0));
            yield return new TestCaseData(Size.Create(0, 1));
            yield return new TestCaseData(Size.Create(10, 10));
            yield return new TestCaseData(Size.Create(4, 100));
            yield return new TestCaseData(Size.Create(5000, 5));
            yield return new TestCaseData(Size.Create(100, 100));
        }

        private static IEnumerable<TestCaseData> IntersectingTestCases()
        {
            IPhysObject GetObjectWithSize(Vector cords, Size size)
            {
                var obj = Substitute.For<IPhysObject>();
                obj.Cords.Returns(cords);
                obj.Size.Returns(size);

                return obj;
            }

            var landscape = Landscape.CreateFromText(new List<string>
            {
                "........................................",
                "........................................",
                "........................................",
                "........................................",
                "........................................",
                "...........................*************",
                "........................****************",
                "..................................******",
                ".................................*******",
                ".**............................*********",
                "*****.............******....************",
                "******.........*************************",
                "****************************************"
            });
            
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(0, 0), Size.Create(5, 5)), false).SetName("int 1");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(2, 2), Size.Create(5, 5)), false).SetName("int 2");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(5, 5), Size.Create(5, 5)), false).SetName("int 3");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(30, 1), Size.Create(15, 15)), true).SetName("int 4");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(26, 7), Size.Create(2, 2)), false).SetName("int 5");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(35, 9), Size.Create(5, 5)), true).SetName("int 6");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(26, 5), Size.Create(1, 1)), false).SetName("int 7");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(-10, 20), Size.Create(100, 100)), false).SetName("int 8");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(-10, 10), Size.Create(100, 100)), true).SetName("int 9");
            
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(26.1, 5), Size.Create(1, 1)), true).SetName("double 1");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(-1, 5), Size.Create(10, 10)), true).SetName("double 2");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(26.7, 7), Size.Create(1, 1)), false).SetName("double 3");
            yield return new TestCaseData(landscape, GetObjectWithSize(Vector.Create(26, 7.1), Size.Create(1, 1)), false).SetName("double 4");
        }

        [TestCaseSource(nameof(CreateSizeTestCases))]
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
        [Repeat(10)]
        public void GetCellSetCell_WorkingWithValues_ShouldReturnRightValues()
        {
            var random = new Random();
            var landscape = Landscape.Create(Size.Create(random.Next(10, 100), random.Next(10, 100)));
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

        [Test]
        public void CreateFromText_ValidText_ShouldReturnRightLandscape()
        {
            var text = new List<string>
            {
                ".............***....",
                ".....**.....*.*.*...",
                "..*.................",
                "........***...****..",
                "..**......*..*****..",
                ".**.**.......*****..",
                "**...**.....******..",
                "********************"
            };

            var sut = Landscape.CreateFromText(text);

            for (var i = 0; i < text.Count; i++)
            {
                for (var j = 0; j < text[i].Length; j++)
                    sut.GetCell(i, j).Should().BeEquivalentTo(text[i][j] == '*' ? GroundCell.Ground : GroundCell.Empty);
            }
        }

        [Test]
        public void IntersectsWith_IntersectingWithLandscape_ShouldThrowArgumentException()
        {
            Action a = () =>
            {
                Landscape.Create(Size.Create(10, 10))
                    .IntersectsWith(Landscape.Create(Size.Create(10, 10)));
            };

            a.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(IntersectingTestCases))]
        public void IntersectsWith_IntersectingWithObject_ShouldReturnExpectedAnswer(Landscape landscape, IPhysObject obj, bool expected)
        {
            var sut = landscape.IntersectsWith(obj);

            sut.Should().Be(expected);
        }
    }
}