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

            var raceData = RaceDataProcessor.ProcessRaceData(args[0], out var bestLapOverall);

            if (!raceData.Any()) return;

            var outputFile = new StreamWriter("result.txt");
            var position = 1;
            outputFile.WriteLine("posição|piloto|voltas|tempo total de prova|melhor volta|tempo após primeiro colocado|velocidade média");
            foreach (var pilotData in raceData)
            {
                outputFile.WriteLine($"{position++}|{pilotData.Id} - {pilotData.Name}" +
                                     $"|{pilotData.CurrentLap}|{pilotData.TotalTime}" +
                                     $"|{pilotData.BestLap}|{pilotData.FinishAt.Subtract(raceData[0].FinishAt)}" +
                                     $"|{pilotData.AvgSpeed:0.000}");
            }

            outputFile.WriteLine();
            outputFile.WriteLine($"Melhor volta da corrida => {bestLapOverall}");

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
