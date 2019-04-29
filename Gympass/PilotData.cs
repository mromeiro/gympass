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

        public TimeSpan TotalTime => TimeSpan.FromMilliseconds(LapsInfo.Sum(lap => lap.LapTime.TotalMilliseconds));

        public TimeSpan BestLap { get; set; }

        public TimeSpan FinishAt { get; set; }

        public decimal AvgSpeed => LapsInfo.Sum(lap => lap.AvgSpeed) / LapsInfo.Count;
    }
}
