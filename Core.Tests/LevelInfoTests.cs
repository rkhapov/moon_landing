using NUnit.Framework;
using FluentAssertions;
using Core.Game;
using Core.Tools;

namespace Core.Tests
{
    [TestFixture]
    public class LevelInfoTests
    {
        [Test]
        public void ReadLevelInfoFromText()
        {
            var str = new string[] { "-1 1", "1,120 -1,600", "100", "moon", "picture1.png" };
            var expected = new LevelInfo(Vector.Create(-1, 1), Vector.Create(1.12, -1.6), 100, "moon", "picture1.png");
            
            var sut = LevelInfo.CreateFromText(str);

            sut.StartPosition.Should().BeEquivalentTo(expected.StartPosition);
            sut.StartVelocity.Should().BeEquivalentTo(expected.StartVelocity);
            sut.StartFuel.Should().BeInRange(expected.StartFuel - 1e-6, expected.StartFuel + 1e-6);
            sut.LandscapeFile.Should().BeEquivalentTo(expected.LandscapeFile);
            sut.PhysicsName.Should().BeEquivalentTo(expected.PhysicsName);
        }
    }
}
