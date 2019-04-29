using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Gympass
{
    public class RaceDataProcessor
    {
        public static List<PilotData> ProcessRaceData(string fileName, out TimeSpan bestLapOverall)
        {
            bestLapOverall = new TimeSpan(1,0,0,0);
            string line;
            var raceData = new Dictionary<int, PilotData>();

            var file = new StreamReader(fileName);
            line = file.ReadLine(); //Discards the first line
            while ((line = file.ReadLine()) != null)
            {
                var lap = Lap.ParseLap(line);

                //Disregards all laps after the fourth one
                if (lap.LapNumber > 4) continue;

                if (raceData.TryGetValue(lap.PilotId, out var pilotData))
                {
                    pilotData.LapsInfo.Add(lap);

                    //Just to be safe in case the laps are not in order
                    if (lap.LapNumber > pilotData.CurrentLap)
                    {
                        pilotData.CurrentLap = lap.LapNumber;
                        pilotData.FinishAt = lap.FinishAt;
                    }

                    pilotData.BestLap = lap.LapTime.CompareTo(pilotData.BestLap) < 0 ? lap.LapTime : pilotData.BestLap;
                }
                else
                {
                    raceData[lap.PilotId] = new PilotData()
                    {
                        CurrentLap = lap.LapNumber,
                        Id = lap.PilotId,
                        Name = lap.PilotName,
                        LapsInfo = new List<Lap> { lap },
                        BestLap = lap.LapTime,
                        FinishAt = lap.FinishAt,
                    };
                }

                bestLapOverall = lap.LapTime.CompareTo(bestLapOverall) < 0 ? lap.LapTime : bestLapOverall;
            }

            //Order the result  by number of laps completed then by the lowest race time
            return raceData.OrderByDescending(_ => _.Value.CurrentLap).
                ThenBy(_ => _.Value.TotalTime.TotalMilliseconds).Select(_ => _.Value).ToList();
        }
    }
}
