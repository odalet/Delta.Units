using static Delta.Units.SpecialCharacters;
using static Delta.Units.DimensionFormulaIndices;
using System.Collections.Generic;

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

        public static Dimension[] All { get; }

        public static Dimension Length { get; }
        public static Dimension Mass { get; }
        public static Dimension Time { get; }
        public static Dimension ElectricCurrent { get; } 
        public static Dimension ThermodynamicTemperature { get; }
        public static Dimension AmountOfSubstance { get; }
        public static Dimension LuminousIntensity { get; }
        public static Dimension None { get; }

        internal static int Count { get; }

        internal static string[] Symbols { get; }

        private static Dimension Register(Dimension dimension)
        {
            all.Add(dimension);
            return dimension;
        }
    }
}
