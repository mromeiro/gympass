using System;
using System.Globalization;

namespace Gympass
{
    public class Lap
    {
        public int PilotId { get; set; }

        public string PilotName { get; set; }

        public int LapNumber { get; set; }

        public TimeSpan LapTime { get; set; }

        public TimeSpan FinishAt { get; set; }

        public decimal AvgSpeed { get; set; }

        public static Lap ParseLap(string line)
        {

            try
            {
                var pilotData = line.Substring(18, 40).TrimEnd();

                var pilotName = pilotData.Split('–')[1].TrimStart();
                var pilotId = int.Parse(pilotData.Split('–')[0].TrimEnd());
                var lapNumber = int.Parse(line.Substring(58, 14).TrimEnd());
                var lapTime = TimeSpan.Parse($"0:0:{line.Substring(72, 32).TrimEnd()}", new CultureInfo("en-US"));
                var finishAt = TimeSpan.Parse(line.Substring(0, 12).TrimEnd(), new CultureInfo("en-US"));
                var avgSpeed = decimal.Parse(line.Substring(104, line.Length - 104).Replace(",","."));

                return new Lap()
                {
                    PilotId = pilotId,
                    PilotName = pilotName,
                    LapNumber = lapNumber,
                    LapTime = lapTime,
                    FinishAt = finishAt,
                    AvgSpeed = avgSpeed
                };
            }
            catch(FormatException e)
            {
                Console.WriteLine($"An error occured while parsing line: {line}", e);
                throw e;
            }

        }
    }
}
