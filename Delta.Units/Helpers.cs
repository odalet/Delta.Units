using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Units
{
    internal static class Helpers
    {
        // Some prime numbers to use in GetHashCode implementations (there are 14 of them)
        public static readonly int[] Primes = new[] { 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59 };

        public static bool IsNone(this DimensionFormula formula) => 
            !formula.ExceptNone().Any(exp => exp != 0) && formula.GetNone() != 0 ;

        public static bool IsNone(this Dimension dimension) => dimension == null || dimension.Formula.IsNone();
        public static bool IsNone(this Unit unit) => unit == null || unit.Dimension.IsNone();

        private static IEnumerable<int> ExceptNone(this DimensionFormula formula) => formula.Take(formula.Count - 1);
        private static int GetNone(this DimensionFormula formula) => formula.Last();

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

        /// <summary>
        /// Returns the function that is (F°G)^pow
        /// </summary>
        /// <param name="f">The outer function in f(g(x)).</param>
        /// <param name="g">The inner function in f(g(x)).</param>
        /// <param name="f_">The inverse of f.</param>
        /// <param name="g_">The inverse of g.</param>
        /// <param name="pow">The pow.</param>
        /// <returns>A new function</returns>
        internal static Func<double, double> CombinePow(
            Func<double, double> f, Func<double, double> g,
            Func<double, double> f_, Func<double, double> g_,
            int pow)
        {
            if (pow == 0) return _ => 1.0; // This returns the 1-constant function.

            // Any null function is considered to be the identity.
            var F = f ?? (x => x);
            var G = g ?? (x => x);
            var F_ = f_ ?? (x => x);
            var G_ = g_ ?? (x => x);

            Func<double, double> FG;
            if (pow < 0) // negative pow: We need find the inverse of F°G; this is G^-1 ° F^-1
                FG = x => G_(F_(x));
            else FG = x => F(G(x)); // otherwise, this is F°G

            var absPow = pow < 0 ? -pow : pow;
            if (absPow == 1) return FG;

            return x =>
            {
                var y = x;
                for (int i = 0; i < absPow; i++)
                    y = FG(y);
                return y;
            };
        }
    }
}
