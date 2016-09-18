using System;
using static Delta.Units.Systems.Aliases;

namespace TestApp
{
    internal static class Program
    {
        static void Main()
        {
            var M = 0.01 * kg;            

            Console.WriteLine($"M = {M}");
            Console.WriteLine($"M = {M.ConvertTo(g)}");

            var s1 = M + 2.0 * M.ConvertTo(g);
            var s2 = 2.0 * M.ConvertTo(g) + M;

            Console.WriteLine("s1 = " + s1);
            Console.WriteLine("s2 = " + s2);

            Console.ReadKey();
            ////var M = 0.001 * Kilogram;
            ////BaseUnit MeterPerSecond = Metre / Second;
            ////Quantity c = 299792458 * MeterPerSecond;

            ////Quantity E = M * c.Pow(2);
            ////Quantity expected = (0.001 * 299792458 * 299792458) * J;
        }
    }
}
