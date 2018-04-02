using System.Linq;

namespace Delta.Units
{
    /// <summary>
    /// A <see cref="Dimension"/> stores the dimension formula associated with a <see cref="Unit"/>.
    /// </summary>
    public sealed class Dimension
    {
        // For now, only this assembly is allowed to create dimensions

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension"/> class.
        /// </summary>
        /// <param name="name">The dimension name.</param>
        /// <param name="symbol">The symbol associated with this dimension.</param>
        /// <param name="formula">The dimension formula.</param>
        internal Dimension(string name, string symbol, DimensionFormula formula)
        {
            Name = name;
            Symbol = symbol;
            Formula = formula.All(exp => exp == 0) ? DimensionFormula.None : formula;
        }

        /// <summary>
        /// Gets this <see cref="Dimension"/> name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the symbol that represents this <see cref="Dimension"/>.
        /// </summary>
        public string Symbol { get; }

        internal DimensionFormula Formula { get; }

        #region Operations on Dimensions

        /// <summary>
        /// Returns a new dimension that is this dimension^<paramref name="exponent"/>.
        /// </summary>
        /// <param name="exponent">The exponent.</param>
        /// <returns>A new <see cref="Dimension"/> instance.</returns>
        public Dimension Pow(int exponent)
        {
            var newName = this.IsNone() ? string.Empty : $"{Name} ^ {exponent}";
            var newSymbol = this.IsNone() ? string.Empty : $"{Symbol}^{exponent}";

            var newFormula = Formula.Clone();
            for (var index = 0; index < newFormula.Count; index++)
                newFormula[index] *= exponent;
            
            return new Dimension(newName, newSymbol, newFormula);
        }

        /// <summary>
        /// Multiplies this dimension by the specified <paramref name="other"/> dimension.
        /// </summary>
        /// <param name="other">The other dimension.</param>
        /// <returns>A new <see cref="Dimension"/> instance.</returns>
        public Dimension MultiplyBy(Dimension other)
        {
            var operand = other ?? BaseDimensions.None;

            var newName = Helpers.CreateDimensionName(this, other, "*");
            var newSymbol = Helpers.CreateDimensionSymbol(this, other, ".");

            var newFormula = Formula.Clone();
            for (var index = 0; index < newFormula.Count; index++)
                newFormula[index] += operand.Formula[index];
            
            return new Dimension(newName, newSymbol, newFormula);
        }

        /// <summary>
        /// Divides this dimension by the specified <paramref name="other"/> dimension.
        /// </summary>
        /// <param name="other">The other dimension.</param>
        /// <returns>A new <see cref="Dimension"/> instance.</returns>
        public Dimension DivideBy(Dimension other)
        {
            var operand = other ?? BaseDimensions.None;

            var newName = Helpers.CreateDimensionName(this, other, "/");
            var newSymbol = Helpers.CreateDimensionSymbol(this, other, "/");

            var newFormula = Formula.Clone();
            for (var index = 0; index < newFormula.Count; index++)
                newFormula[index] -= operand.Formula[index];
            
            return new Dimension(newName, newSymbol, newFormula);
        }

        #endregion

        #region Comparison, Equality

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as Dimension;
            if (other is null) // Considered to be the 'None' dimension
                return Formula.IsNone();

            if (ReferenceEquals(this, other)) return true;

            return Formula.Equals(other.Formula);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Formula.GetHashCode();

        #endregion

        #region Operators overloads

        /// <inheritdoc />
        public static Dimension operator *(Dimension left, Dimension right) =>
            (left ?? BaseDimensions.None).MultiplyBy(right);

        /// <inheritdoc />
        public static Dimension operator /(Dimension left, Dimension right) =>
            (left ?? BaseDimensions.None).DivideBy(right);

        // Beware, ^ priority is not the mathematical one! It should always be used with parens to avoid mistakes
        // This is because, natively, ^ is the XOR operator, not the pow one... I miss an ** operator in C# 

        /// <summary>
        /// Implements the operator ^ on a dimension and an integer exponent. NB: always enclose these expressions in parentheses.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator ^(Dimension dimension, int exponent) =>
            (dimension ?? BaseDimensions.None).Pow(exponent);

        /// <inheritdoc />
        public static bool operator ==(Dimension left, Dimension right) =>
            (left ?? BaseDimensions.None).Equals(right);

        /// <inheritdoc />
        public static bool operator !=(Dimension left, Dimension right) =>
            !(left == right);

        #endregion

        /// <inheritdoc />
        public override string ToString() => this.IsNone() ? string.Empty : $"{Symbol} ({Name})";
    }
}
