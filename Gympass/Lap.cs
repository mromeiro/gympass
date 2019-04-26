using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gympass
{
    class Lap
    {
        int PilotId { get; set; }

        string PilotName { get; set; }

        int LapNumber { get; set; }

        TimeSpan lapTime { get; set; }

        public static Lap ParseLap(string line)
        {
            var pilotData = line.Substring(18, 40).TrimEnd();

            var pilotName = pilotData.Split('–')[1].TrimStart();
            var pilotId = int.Parse(pilotData.Split('–')[0].TrimEnd());
            var lapNumber = int.Parse(line.Substring(58, 14).TrimEnd());
            var lapTime = TimeSpan.Parse($"0:0:{line.Substring(72,32).TrimEnd()}", new CultureInfo("en-US"));

            return new Lap()
            {
                PilotId = pilotId,
                PilotName = pilotName,
                LapNumber = lapNumber,
                lapTime = lapTime
            };
        }
    }
}
