using System;
using static Delta.Units.Systems.Aliases;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var M = 0.001 * kg;
            Console.WriteLine($"M = {M}");

            Console.ReadKey();
            ////var M = 0.001 * Kilogram;
            ////BaseUnit MeterPerSecond = Metre / Second;
            ////Quantity c = 299792458 * MeterPerSecond;

            ////Quantity E = M * c.Pow(2);
            ////Quantity expected = (0.001 * 299792458 * 299792458) * J;
        }
    }
}
