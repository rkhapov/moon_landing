using System;
using MoonLanding.Tools;
using NUnit.Framework;

namespace MoonLanding.Tests
{
    [TestFixture]
    public class LandscapeTests
    {
        [Test]
        public void Velocity_SettingValue_ShouldBeIgnored()
        {
            var landscape = Landscape.Create(Size.Create(10, 10));

        }
    }
}