using System.Collections;
using System.Collections.Generic;
using static Delta.Units.DimensionFormulaIndices;

namespace Delta.Units
{
    internal static class DimensionFormulaIndices
    {
        // Indices of the exponents array
        public static readonly int l = 0;
        public static readonly int m = 1;
        public static readonly int t = 2;
        public static readonly int i = 3;
        public static readonly int th = 4;
        public static readonly int n = 5;
        public static readonly int j = 6;
        public static readonly int z = 7;
    }

    /// <summary>
    /// This stores a Dimension formula.
    /// </summary>
    /// <remarks>
    /// The formula is a product of base dimensions, each of which can have an (integer) exponent.
    /// These exponents are stored in an array at a spcefic index. Each base dimension 
    /// is associated a well-known index. The base dimensions are always given in this order:
    /// L, M, T, I, Th, N, J
    /// Examples:
    /// <list type="bullet">
    /// <item>
    /// The 'Length' dimension is defined by this array: [1, 0, 0, 0, 0, 0, 0] meaning, it is L^1 = L
    /// </item>
    /// <item>
    /// The 'Velocity' dimension is a compound of Length and Time:
    /// [1, 0, -1, 0, 0, 0, 0] which reads L^1 * T^-1 = L/T
    /// </item>
    /// <item>
    /// The 'None' dimension (for dimension-less quantities such as angles or proportions is defined as
    /// [0, 0, 0, 0, 0, 0, 0]
    /// </item>
    /// </list>
    /// </remarks>
    internal sealed class DimensionFormula : IEnumerable<int>
    {
        private readonly int[] exponents = new int[BaseDimensions.Count];

        // Formulas for base dimensions
        public static DimensionFormula Length { get; } = new DimensionFormula { L = 1 };
        public static DimensionFormula Mass { get; } = new DimensionFormula { M = 1 };
        public static DimensionFormula Time { get; } = new DimensionFormula { T = 1 };
        public static DimensionFormula ElectricCurrent { get; } = new DimensionFormula { I = 1 };
        public static DimensionFormula ThermodynamicTemperature { get; } = new DimensionFormula { Th = 1 };
        public static DimensionFormula AmountOfSubstance { get; } = new DimensionFormula { N = 1 };
        public static DimensionFormula LuminousIntensity { get; } = new DimensionFormula { J = 1 };
        public static DimensionFormula None { get; } = new DimensionFormula() { Z = 1 };

        /// <summary>
        /// Prevents a default instance of the <see cref="DimensionFormula"/> class from being created.
        /// </summary>
        private DimensionFormula() { }

        public int Count => exponents.Length;

        public int this[int index]
        {
            get { return exponents[index]; }
            set { exponents[index] = value; }
        }

        public int L
        {
            get { return exponents[l]; }
            set { exponents[l] = value; }
        }

        public int M
        {
            get { return exponents[m]; }
            set { exponents[m] = value; }
        }

        public int T
        {
            get { return exponents[t]; }
            set { exponents[t] = value; }
        }

        public int I
        {
            get { return exponents[i]; }
            set { exponents[i] = value; }
        }

        public int Th
        {
            get { return exponents[th]; }
            set { exponents[th] = value; }
        }

        public int N
        {
            get { return exponents[n]; }
            set { exponents[n] = value; }
        }

        public int J
        {
            get { return exponents[j]; }
            set { exponents[j] = value; }
        }

        public int Z
        {
            get { return exponents[z]; }
            set { exponents[z] = value; }
        }

        public override bool Equals(object obj)
        {
            var other = obj as DimensionFormula;
            if (other == null) // Considered to be the 'None' dimension
                return this.IsNone();

            if (ReferenceEquals(this, other)) return true;

            // Let's compare the formulas
            for (var index = 0; index < Count; index++)
            {
                if (this[index] != other[index])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = 0;
            for (int index = 0; index < Count; index++)
                hash ^= exponents[index] * Helpers.Primes[index];
            return hash;
        }

        // In a PCL, the ICloneable interface does not exist any more.
        public DimensionFormula Clone()
        {
            var result = new DimensionFormula();
            for (var index = 0; index < Count; index++)
                result.exponents[index] = exponents[index];
            return result;
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var exponent in exponents) yield return exponent;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<int>)this).GetEnumerator();
    }
}
