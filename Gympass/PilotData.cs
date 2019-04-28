using System;
using System.Collections.Generic;
using System.Linq;

namespace Gympass
{
    public class PilotData
    {
        public int Id { get; set; }

        public string Name { get; set;}

        public int CurrentLap { get; set; }

        public ICollection<Lap> LapsInfo { get; set; }

        public TimeSpan TotalTime => TimeSpan.FromMilliseconds(LapsInfo.Sum(lap => lap.lapTime.TotalMilliseconds));
    }
}
