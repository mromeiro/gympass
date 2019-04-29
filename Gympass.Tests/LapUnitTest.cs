using NUnit.Framework;
using Gympass;
using System;

namespace Tests
{
    [TestFixture]
    public class LapUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseLapTest()
        {
            var line = "23:49:08.277      038 – F.MASSA                           1             1:02.852                        44,275";

            var result = Lap.ParseLap(line);

            Assert.AreEqual(result.LapNumber, 1);
            Assert.AreEqual(result.LapTime, new TimeSpan(0,0,1,2,852));
            Assert.AreEqual(result.PilotId, 38);
            Assert.AreEqual(result.PilotName, "F.MASSA");
            Assert.AreEqual(result.AvgSpeed, decimal.Parse("44.275"));
            Assert.AreEqual(result.FinishAt, new TimeSpan(0, 23, 49, 8, 277));
        }


        [Test]
        public void ParseLapErrorTest()
        {
            var line = "23:49:08.277      038 – F.MASSA                           A             1:02.852                        44,275";

            Assert.Throws<FormatException>(() => Lap.ParseLap(line));
        }
    }
}