using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Gympass
{
    public class RaceDataProcessor
    {
        public static List<PilotData> ProcessRaceData(string fileName)
        {
            string line;
            var raceData = new Dictionary<int, PilotData>();

            var file = new StreamReader(fileName);
            line = file.ReadLine(); //Discards the first line
            while ((line = file.ReadLine()) != null)
            {
                var lap = Lap.ParseLap(line);

                if (raceData.TryGetValue(lap.PilotId, out var pilotData))
                {
                    pilotData.LapsInfo.Add(lap);

                    //Just to be safe in case the laps are not in order
                    pilotData.CurrentLap = pilotData.CurrentLap < lap.LapNumber ? lap.LapNumber : pilotData.CurrentLap;
                }
                else
                {
                    raceData[lap.PilotId] = new PilotData()
                    {
                        CurrentLap = lap.LapNumber,
                        Id = lap.PilotId,
                        Name = lap.PilotName,
                        LapsInfo = new List<Lap> { lap }
                    };
                }
            }

            //Order the result  by number of laps completed then by the lowest race time
            return raceData.OrderByDescending(_ => _.Value.CurrentLap).
                ThenBy(_ => _.Value.TotalTime.TotalMilliseconds).Select(_ => _.Value).ToList();
        }
    }
}
