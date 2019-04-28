using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gympass
{
    class Program
    {
        static void Main(string[] args)
        {
            var raceData = new Dictionary<int, PilotData>();
            string line;

            if (!CheckParams(args)) Environment.Exit(1);

            var file = new StreamReader(args[0]);
            line = file.ReadLine(); //Discards the first line
            while ((line = file.ReadLine()) != null)
            {
                var lap = Lap.ParseLap(line);

                if(raceData.TryGetValue(lap.PilotId, out var pilotData))
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
            var pilotDataOrdered = raceData.OrderByDescending(_ => _.Value.CurrentLap).
                ThenBy(_ => _.Value.TotalTime.TotalMilliseconds).Select(_ => _.Value);

            var outputFile = new StreamWriter("result.txt");
            var position = 1;
            outputFile.WriteLine("posição|piloto|voltas|tempo total de prova");
            foreach (var pilotData in pilotDataOrdered)
            {
                outputFile.WriteLine($"{position++}|{pilotData.Id} - {pilotData.Name}|{pilotData.CurrentLap}|{pilotData.TotalTime}");
            }

            outputFile.Close();
            Environment.Exit(0);
        }

        private static bool CheckParams(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please inform the name of the input file");
                return false;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File {args[0]} does not exist.");
                return false;
            }

            return true;
        }
    }
}
