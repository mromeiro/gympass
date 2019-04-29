using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Gympass.Tests
{
    [TestFixture]
    class RaceDataProcessorUnitTest
    {
        [Test]
        public void ProcessRace_ShoudReturnPilotsInOrder()
        {
            var raceData = RaceDataProcessor.ProcessRaceData("resources/input.txt", out var bestLapOverall);

            Assert.IsInstanceOf<List<PilotData>>(raceData);

            Assert.AreEqual(raceData.Count, 6);

            Assert.AreEqual(raceData[0].Id, 38);
            Assert.AreEqual(raceData[1].Id, 2);
            Assert.AreEqual(raceData[2].Id, 33);
            Assert.AreEqual(raceData[3].Id, 23);
            Assert.AreEqual(raceData[4].Id, 15);
            Assert.AreEqual(raceData[5].Id, 11);

            Assert.AreEqual(raceData[0].CurrentLap, 4);
            Assert.AreEqual(raceData[0].Name, "F.MASSA");
            Assert.AreEqual(raceData[0].LapsInfo.Count, 4);
            Assert.AreEqual(raceData[0].BestLap, new TimeSpan(0, 0, 1, 2, 769));
            Assert.AreEqual(raceData[0].FinishAt, new TimeSpan(0, 23, 52, 17, 3));

            Assert.AreEqual(bestLapOverall, new TimeSpan(0,0,1,2,769));
        }
    }
}
