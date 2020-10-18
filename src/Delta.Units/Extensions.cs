using System.Collections.Generic;
using System.Linq;

namespace Delta.Units
{
    /// <summary>
    /// Set of useful extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the symbol associated with the base dimension at the specified index.
        /// </summary>
        /// <param name="index">The base dimension index.</param>
        /// <returns>A string containing the symbol associated with the requested base dimension.</returns>
        public static string GetBaseDimensionSymbol(int index) => BaseDimensions.Symbols[index];

        /// <summary>
        /// Gets a string representation of the specified dimension.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <returns>A string containing a human-readable representation of <paramref name="dimension"/>.</returns>
        public static string GetFormulaAsString(this Dimension dimension)
        {
            var dim = dimension ?? BaseDimensions.None;
            var parts = dim.Formula
                .Select((exp, index) =>
                {
                    if (exp == 0)
                        return string.Empty;
                    return exp == 1 ? GetBaseDimensionSymbol(index) : $"{GetBaseDimensionSymbol(index)}^{exp}";
                })
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

        /// <summary>
        /// Determines whether the two specified units are compatible (ie they share the same dimension formula).
        /// </summary>
        /// <param name="unit">A unit.</param>
        /// <param name="other">Another unit.</param>
        /// <returns>
        ///   <c>true</c> if the units are compatible; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompatibleWith(this Unit unit, Unit other) => Unit.AreCompatible(unit, other);

        /// <summary>
        /// Converts the specified <paramref name="value"/> expressed in the <paramref name="unit"/> unit to a value expressed in the <paramref name="other"/> unit.
        /// </summary>
        /// <param name="unit">The value's unit.</param>
        /// <param name="value">The value.</param>
        /// <param name="other">The converted value's unit.</param>
        /// <returns>A value equivalent to <paramref name="value"/>, but expressed using <paramref name="other"/> unit.</returns>
        public static decimal ConvertTo(this Unit unit, decimal value, Unit other) => Unit.Convert(value, unit, other);

        /// <summary>
        /// Determines whether the specified dimension formula represents a dimensionless quantity.
        /// </summary>
        /// <param name="formula">The dimension formula to test.</param>
        /// <returns>
        ///   <c>true</c> if the specified dimension formula is dimensionless; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNone(this DimensionFormula formula) =>
           !formula.ExceptNone().Any(exp => exp != 0) && formula.GetNone() != 0;

        /// <summary>
        /// Determines whether the specified dimension represents a dimensionless quantity.
        /// </summary>
        /// <param name="dimension">The dimension to test.</param>
        /// <returns>
        ///   <c>true</c> if the specified dimension is dimensionless; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNone(this Dimension dimension) => dimension == null || dimension.Formula.IsNone();

        /// <summary>
        /// Determines whether the specified unit is dimensionless.
        /// </summary>
        /// <param name="unit">The unit to test.</param>
        /// <returns>
        ///   <c>true</c> if the specified unit is dimensionless; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNone(this Unit unit) => unit == null || unit.Dimension.IsNone();
        
        private static IEnumerable<int> ExceptNone(this DimensionFormula formula) => formula.Take(formula.Count - 1);
        private static int GetNone(this DimensionFormula formula) => formula.Last();
    }
}
