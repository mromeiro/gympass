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
            string line;
            if (args.Length == 0)
            {
                Console.WriteLine("Please inform the name of the input file");
                Environment.Exit(1);
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File {args[0]} does not exist.");
                Environment.Exit(1);
            }

            var file = new StreamReader(args[0]);
            line = file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                var lap = Lap.ParseLap(line);
            }

            Environment.Exit(0);
        }
    }
}
