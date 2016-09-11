using System.Collections.Generic;
using System.Linq;

namespace Delta.Units
{
    public static class Extensions
    {
        public static string GetBaseDimensionSymbol(int index) => BaseDimensions.Symbols[index];

        public static string GetFormulaAsString(this Dimension dimension)
        {
            var dim = dimension ?? BaseDimensions.None;
            var parts = dim.Formula
                .Select((exp, index) => exp == 0 ? 
                    string.Empty : 
                    (exp == 1 ? 
                        GetBaseDimensionSymbol(index) : 
                        $"{GetBaseDimensionSymbol(index)}^{exp}"))
                .Where(s => !string.IsNullOrEmpty(s));

            var result = string.Join(".", parts);

            // If the result is Z with any exponent, it is simply Z.
            if (result.StartsWith("Z")) return "Z";

            // If the resulting formula ends being multiplied by Z with any exponent, remove it            
            var location = result.IndexOf(".Z");
            if (location > 0)
                result = result.Substring(0, location);
            
            return result;
        }

        public static bool IsCompatibleWith(this Unit unit, Unit other) => Unit.AreCompatible(unit, other);

        public static double ConvertTo(this Unit unit, double value, Unit other) => Unit.Convert(value, unit, other);

        public static bool IsNone(this DimensionFormula formula) =>
           !formula.ExceptNone().Any(exp => exp != 0) && formula.GetNone() != 0;

        public static bool IsNone(this Dimension dimension) => dimension == null || dimension.Formula.IsNone();
        public static bool IsNone(this Unit unit) => unit == null || unit.Dimension.IsNone();

        private static IEnumerable<int> ExceptNone(this DimensionFormula formula) => formula.Take(formula.Count - 1);
        private static int GetNone(this DimensionFormula formula) => formula.Last();
    }
}
