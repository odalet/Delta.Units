using System.Collections.Generic;
using static Delta.Units.DimensionFormulaIndices;
using static Delta.Units.SpecialCharacters;

namespace Delta.Units
{
    /// <summary>
    /// Base dimensions (as defined by SI)
    /// </summary>
    /// <remarks>
    /// These are the dimensions defined by SI (see https://en.wikipedia.org/wiki/SI_base_unit) +
    /// the none dimension (used for dimension-less quantities);
    /// </remarks>
    public static class BaseDimensions
    {
        private static readonly List<Dimension> all;

        static BaseDimensions()
        {
            all = new List<Dimension>();
            Symbols = new[] { "L", "M", "T", "I", Theta, "N", "J", "Z" };
            Count = Symbols.Length;

            Length = Register(new Dimension("Length", Symbols[l], DimensionFormula.Length));
            Mass = Register(new Dimension("Mass", Symbols[m], DimensionFormula.Mass));
            Time = Register(new Dimension("Time", Symbols[t], DimensionFormula.Time));
            ElectricCurrent = Register(new Dimension("Electric Current", Symbols[i], DimensionFormula.ElectricCurrent));
            ThermodynamicTemperature = Register(new Dimension("Thermodynamic Temperature", Symbols[th], DimensionFormula.ThermodynamicTemperature));
            AmountOfSubstance = Register(new Dimension("Amount of Substance", Symbols[n], DimensionFormula.AmountOfSubstance));
            LuminousIntensity = Register(new Dimension("Luminous Intensity", Symbols[j], DimensionFormula.LuminousIntensity));
            None = Register(new Dimension("None", Symbols[z], DimensionFormula.None));

            All = all.ToArray();
        }

        /// <summary>
        /// Gets the array of all existing base dimensions.
        /// </summary>
        public static Dimension[] All { get; }

        /// <summary>
        /// Gets the 'Mass' base dimension.
        /// </summary>
        public static Dimension Length { get; }

        /// <summary>
        /// Gets the 'Length' base dimension.
        /// </summary>
        public static Dimension Mass { get; }

        /// <summary>
        /// Gets the 'Time' base dimension.
        /// </summary>
        public static Dimension Time { get; }

        /// <summary>
        /// Gets the 'Electric Current' base dimension.
        /// </summary>
        public static Dimension ElectricCurrent { get; }

        /// <summary>
        /// Gets the 'Thermodynamic Temperature' base dimension.
        /// </summary>
        public static Dimension ThermodynamicTemperature { get; }

        /// <summary>
        /// Gets the 'Amount of Substance' base dimension.
        /// </summary>
        public static Dimension AmountOfSubstance { get; }

        /// <summary>
        /// Gets the 'Luminous Intensity' base dimension.
        /// </summary>
        public static Dimension LuminousIntensity { get; }

        /// <summary>
        /// Gets the special 'None' base dimension (used to tag dimensionless quantities).
        /// </summary>
        public static Dimension None { get; }

        /// <summary>
        /// Gets the number of base dimensions.
        /// </summary>
        internal static int Count { get; }

        /// <summary>
        /// Gets the symbols assciated with base dimensions.
        /// </summary>
        internal static string[] Symbols { get; }

        private static Dimension Register(Dimension dimension)
        {
            all.Add(dimension);
            return dimension;
        }
    }
}
