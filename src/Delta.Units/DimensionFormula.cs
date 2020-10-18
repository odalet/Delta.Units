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
    /// This stores a <see cref="Dimension"/> formula.
    /// </summary>
    /// <remarks>
    /// The formula is a product of base dimensions, each of which can have an (integer) exponent.
    /// These exponents are stored in an array at a spcefic index.Each base dimension
    /// is associated a well-known index. The base dimensions are always given in this order:
    ///
    /// * **L** (Length), 
    /// * **M** (Mass), 
    /// * **T** (Time), 
    /// * **I** (Electric Current), 
    /// * **Θ** (Thermodynamic Temperature), 
    /// * **N** (Amount of Substance), 
    /// * **J** (Luminous Intensity)
    /// * **Z** (this one is not a SI dimension but is used to represent dimension-less quantities)
    ///
    /// _Examples:_
    ///
    /// * The **Length** dimension is defined by this array: **[1, 0, 0, 0, 0, 0, 0, 0]** meaning, it is **L^1 = L**
    /// * The **Velocity** dimension is a compound of **Length** and **Time** dimensions. It is defined as **[1, 0, -1, 0, 0, 0, 0, 0]** which reads **L^1 * T^-1 = L/T**
    /// * The special **None** dimension (for dimension-less quantities such as angles or proportions) is defined as **[0, 0, 0, 0, 0, 0, 0, 1]**
    /// </remarks>
    public sealed class DimensionFormula : IEnumerable<int>
    {
        private readonly int[] exponents = new int[BaseDimensions.Count];

        // Formulas for base dimensions

        /// <summary>
        /// The formula of <see cref="BaseDimensions.Length"/>
        /// </summary>
        public static DimensionFormula Length { get; } = new DimensionFormula { L = 1 };

        /// <summary>
        /// The formula of <see cref="BaseDimensions.Mass"/>
        /// </summary>
        public static DimensionFormula Mass { get; } = new DimensionFormula { M = 1 };

        /// <summary>
        /// The formula of <see cref="BaseDimensions.Time"/>
        /// </summary>
        public static DimensionFormula Time { get; } = new DimensionFormula { T = 1 };

        /// <summary>
        /// The formula of <see cref="BaseDimensions.ElectricCurrent"/>
        /// </summary>
        public static DimensionFormula ElectricCurrent { get; } = new DimensionFormula { I = 1 };

        /// <summary>
        /// The formula of <see cref="BaseDimensions.ThermodynamicTemperature"/>
        /// </summary>
        public static DimensionFormula ThermodynamicTemperature { get; } = new DimensionFormula { Th = 1 };

        /// <summary>
        /// The formula of <see cref="BaseDimensions.AmountOfSubstance"/>
        /// </summary>
        public static DimensionFormula AmountOfSubstance { get; } = new DimensionFormula { N = 1 };

        /// <summary>
        /// The formula of <see cref="BaseDimensions.LuminousIntensity"/>
        /// </summary>
        public static DimensionFormula LuminousIntensity { get; } = new DimensionFormula { J = 1 };

        /// <summary>
        /// The formula of the special <see cref="BaseDimensions.None"/> dimension.
        /// </summary>
        public static DimensionFormula None { get; } = new DimensionFormula() { Z = 1 };

        /// <summary>
        /// Prevents a default instance of the <see cref="DimensionFormula"/> class from being created.
        /// </summary>
        private DimensionFormula() { }

        internal int Count => exponents.Length;

        /// <summary>
        /// Gets or sets the <see cref="int"/> exponent at the specified index.
        /// </summary>
        public int this[int index]
        {
            get => exponents[index];
            set => exponents[index] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the L dimension.
        /// </summary>
        public int L
        {
            get => exponents[l];
            set => exponents[l] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the L dimension.
        /// </summary>
        public int M
        {
            get => exponents[m];
            set => exponents[m] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the T dimension.
        /// </summary>
        public int T
        {
            get => exponents[t];
            set => exponents[t] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the I dimension.
        /// </summary>
        public int I
        {
            get => exponents[i];
            set => exponents[i] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the Th dimension.
        /// </summary>
        public int Th
        {
            get => exponents[th];
            set => exponents[th] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the N dimension.
        /// </summary>
        public int N
        {
            get => exponents[n];
            set => exponents[n] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the J dimension.
        /// </summary>
        public int J
        {
            get => exponents[j];
            set => exponents[j] = value;
        }

        /// <summary>
        /// Gets or sets the exponent for the Z dimension.
        /// </summary>
        public int Z
        {
            get => exponents[z];
            set => exponents[z] = value;
        }

        /// <summary>
        /// Deep-Copies this instance.
        /// </summary>
        /// <returns>A deep copy of this <see cref="DimensionFormula"/>.</returns>
        public DimensionFormula Copy()
        {
            var result = new DimensionFormula();
            for (var index = 0; index < Count; index++)
                result.exponents[index] = exponents[index];
            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is DimensionFormula other)) // Considered to be the 'None' dimension
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

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = 0;
            for (var index = 0; index < Count; index++)
                hash ^= exponents[index] * Helpers.Primes[index];
            return hash;
        }

        /// <inheritdoc />
        public IEnumerator<int> GetEnumerator()
        {
            foreach (var exponent in exponents) yield return exponent;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<int>)this).GetEnumerator();
    }
}
