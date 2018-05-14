using System;

namespace Delta.Units
{
    internal static class Helpers
    {
        // Some prime numbers to use in GetHashCode implementations (there are 14 of them)
        public static readonly int[] Primes = new[] { 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59 };

        // A more accurate value of PI:
        // Math.PI (double) is  : 3.14159265358979
        // We have here         : 3.1415926535897932384626433833
        // That was rounded from: 3.14159265358979323846264338327950288419716...
        public static readonly decimal PI = 3.1415926535897932384626433833m;

        public static string CreateDimensionName(Dimension left, Dimension right, string op)
        {
            if (left.IsNone() && right.IsNone()) return string.Empty;
            if (left.IsNone()) return right.Name;
            if (right.IsNone()) return left.Name;

            return $"{left.Name} {op} {right.Name}";
        }

        public static string CreateDimensionSymbol(Dimension left, Dimension right, string op)
        {
            if (left.IsNone() && right.IsNone()) return string.Empty;
            if (left.IsNone()) return right.Symbol;
            if (right.IsNone()) return left.Symbol;

            return $"{left.Symbol}{op}{right.Symbol}";
        }

        public static string CreateUnitName(Unit left, Unit right, string op)
        {
            if (left.IsNone() && right.IsNone()) return string.Empty;
            if (left.IsNone()) return right.Name;
            if (right.IsNone()) return left.Name;

            return $"{left.Name} {op} {right.Name}";
        }

        public static string CreateUnitSymbol(Unit left, Unit right, string op)
        {
            if (left.IsNone() && right.IsNone()) return string.Empty;
            if (left.IsNone()) return right.Symbol;
            if (right.IsNone()) return left.Symbol;

            return $"{left.Symbol}{op}{right.Symbol}";
        }
    }
}
