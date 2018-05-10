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
            var info = new LevelInfo(Vector.Create(-1, 1), Vector.Create(1.12, -1.6), 100, "moon", "picture1.png");
            var result = LevelInfo.CreateFromText(str);

            result.StartPosition.Should().BeEquivalentTo(info.StartPosition);
            result.StartVelocity.Should().BeEquivalentTo(info.StartVelocity);
            result.StartFuel.Should().BeInRange(info.StartFuel - 1e-6, info.StartFuel + 1e-6);
            result.Landscape.Should().BeEquivalentTo(info.Landscape);
            result.Physics.Should().BeEquivalentTo(info.Physics);
        }
    }
}
