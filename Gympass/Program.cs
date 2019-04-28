using System;
using System.IO;
using System.Linq;

namespace Gympass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!CheckParams(args)) return ;

            var raceData = RaceDataProcessor.ProcessRaceData(args[0]);

            if (!raceData.Any()) return;

            var outputFile = new StreamWriter("result.txt");
            var position = 1;
            outputFile.WriteLine("posição|piloto|voltas|tempo total de prova");
            foreach (var pilotData in raceData)
            {
                outputFile.WriteLine($"{position++}|{pilotData.Id} - {pilotData.Name}|{pilotData.CurrentLap}|{pilotData.TotalTime}");
            }

            outputFile.Close();
        }

        public static bool CheckParams(string[] args)
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
