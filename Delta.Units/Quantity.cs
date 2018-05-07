using System;
using Delta.Units.Globalization;

namespace Delta.Units
{
    /// <summary>
    /// A quantity stores a pair consisting of a <see cref="decimal"/> value and a <see cref="T:Delta.Units.Unit"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="Quantity"/> type supports operators overloads that correctly compute the resulting unit and value.
    /// </remarks>
    public sealed class Quantity : IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Quantity"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        public Quantity(decimal value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Gets this <see cref="Quantity"/>'s value.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Gets this <see cref="Quantity"/> <see cref="Unit"/>.
        /// </summary>
        public Unit Unit { get; }

        /// <summary>
        /// Converts this <see cref="Quantity"/> into a new <see cref="Quantity"/> by using the specified target <see cref="T:Delta.Units.Unit"/>.
        /// </summary>
        /// <param name="target">The target <see cref="T:Delta.Units.Unit"/>.</param>
        /// <returns>
        /// A new instance of <see cref="Quantity"/> 
        /// based on the specified <see cref="T:Delta.Units.Unit"/> and which value was converted using <paramref name="target"/>.
        /// </returns>
        public Quantity ConvertTo(Unit target) => new Quantity(
            Unit.Convert(Value, Unit, target), target);

        #region Operator Overloads

        // double op quantity & quantity op double

        /// <inheritdoc />
        public static Quantity operator +(Quantity left, decimal right) => right + left;
        /// <inheritdoc />
        public static Quantity operator +(decimal left, Quantity right) => new Quantity(left + right.Value, right.Unit);

        /// <inheritdoc />
        public static Quantity operator -(Quantity left, decimal right) => (-1m * right) + left;
        /// <inheritdoc />
        public static Quantity operator -(decimal left, Quantity right) => (-1m * left) + right;

        /// <inheritdoc />
        public static Quantity operator *(Quantity left, decimal right) => right * left;
        /// <inheritdoc />
        public static Quantity operator *(decimal left, Quantity right) => new Quantity(left * right.Value, right.Unit);

        /// <inheritdoc />
        public static Quantity operator /(Quantity left, decimal right) => (1m / right) * left;
        /// <inheritdoc />
        public static Quantity operator /(decimal left, Quantity right) => (1m / left) * right;

        // double-based overloads are defined so that it is easy for the user to define quantities.
        // Ho<ever beware of the precision and floating-point rounding issues

        /// <inheritdoc />
        public static Quantity operator +(Quantity left, double right) => left + (decimal)right;
        /// <inheritdoc />
        public static Quantity operator +(double left, Quantity right) => (decimal)left + right;

        /// <inheritdoc />
        public static Quantity operator -(Quantity left, double right) => left - (decimal)right;
        /// <inheritdoc />
        public static Quantity operator -(double left, Quantity right) => (decimal)left - right;

        /// <inheritdoc />
        public static Quantity operator *(Quantity left, double right) => left * (decimal)right;
        /// <inheritdoc />
        public static Quantity operator *(double left, Quantity right) => (decimal)left * right;

        /// <inheritdoc />
        public static Quantity operator /(Quantity left, double right) => left / (decimal)right;
        /// <inheritdoc />
        public static Quantity operator /(double left, Quantity right) => (decimal)left / right;

        // Because Int32 can be implicitely converted to double or decimal, we need also provide
        // overloads for disambiguation.
        // By the way such disambiguation should also be necessary for other integer types, but 
        // we'll leave it to the user to explicitely cast to int, decimal or double.

        /// <inheritdoc />
        public static Quantity operator +(Quantity left, int right) => left + (decimal)right;
        /// <inheritdoc />
        public static Quantity operator +(int left, Quantity right) => (decimal)left + right;

        /// <inheritdoc />
        public static Quantity operator -(Quantity left, int right) => left - (decimal)right;
        /// <inheritdoc />
        public static Quantity operator -(int left, Quantity right) => (decimal)left - right;

        /// <inheritdoc />
        public static Quantity operator *(Quantity left, int right) => left * (decimal)right;
        /// <inheritdoc />
        public static Quantity operator *(int left, Quantity right) => (decimal)left * right;

        /// <inheritdoc />
        public static Quantity operator /(Quantity left, int right) => left / (decimal)right;
        /// <inheritdoc />
        public static Quantity operator /(int left, Quantity right) => (decimal)left / right;

        // quantity op quantity

        /// <summary>
        /// Implements the operator + on two quantities. By convention, the result is expressed in the left quantity's unit.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator +(Quantity left, Quantity right) => new Quantity(
                left.Value + right.Unit.ConvertTo(right.Value, left.Unit),
                left.Unit);

        /// <summary>
        /// Implements the operator - on two quantities. By convention, the result is expressed in the left quantity's unit.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator -(Quantity left, Quantity right) => new Quantity(
                left.Value - right.Unit.ConvertTo(right.Value, left.Unit),
                left.Unit);

        #endregion

        #region Formatting

        /// <inheritdoc />
        public override string ToString() => QuantityFormatter.Format(this, null, null);

        /// <inheritdoc />
        public string ToString(string format) => QuantityFormatter.Format(this, format, null);

        /// <inheritdoc />
        public string ToString(IFormatProvider formatProvider) => QuantityFormatter.Format(this, null, formatProvider);

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider) => QuantityFormatter.Format(this, format, formatProvider);

        #endregion
    }
}
