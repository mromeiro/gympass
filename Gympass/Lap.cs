using System;
using System.Globalization;

namespace Gympass
{
    public class Lap
    {
        public int PilotId { get; set; }

        public string PilotName { get; set; }

        public int LapNumber { get; set; }

        public TimeSpan lapTime { get; set; }

        public static Lap ParseLap(string line)
        {

            try
            {
                var pilotData = line.Substring(18, 40).TrimEnd();

                var pilotName = pilotData.Split('–')[1].TrimStart();
                var pilotId = int.Parse(pilotData.Split('–')[0].TrimEnd());
                var lapNumber = int.Parse(line.Substring(58, 14).TrimEnd());
                var lapTime = TimeSpan.Parse($"0:0:{line.Substring(72, 32).TrimEnd()}", new CultureInfo("en-US"));

                return new Lap()
                {
                    PilotId = pilotId,
                    PilotName = pilotName,
                    LapNumber = lapNumber,
                    lapTime = lapTime
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
