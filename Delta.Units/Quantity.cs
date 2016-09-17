using System;
using Delta.Units.Globalization;

namespace Delta.Units
{
    /// <summary>
    /// A quantity stores a pair consisting of a <see cref="double"/> value and a <see cref="Unit"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="Quantity"/> types supports operators overloads that correctly compute the resulting unit and value.
    /// </remarks>
    public sealed class Quantity : IFormattable
    {
        public Quantity(double value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public double Value { get; }
        public Unit Unit { get; }

        public Quantity ConvertTo(Unit target) => new Quantity(
            Unit.Convert(Value, Unit, target), target);

        #region Operator Overloads

        // double op quantity & quantity op double

        public static Quantity operator +(Quantity left, double right) => right + left;
        public static Quantity operator +(double left, Quantity right) => new Quantity(left + right.Value, right.Unit);

        public static Quantity operator -(Quantity left, double right) => (-1.0 * right) + left;
        public static Quantity operator -(double left, Quantity right) => (-1.0 * left) + right;

        public static Quantity operator *(Quantity left, double right) => right * left;
        public static Quantity operator *(double left, Quantity right) => new Quantity(left * right.Value, right.Unit);

        public static Quantity operator /(Quantity left, double right) => (1.0 / right) * left;
        public static Quantity operator /(double left, Quantity right) => (1.0 / left) * right;

        // quantity op quantity

        // By convention, the result is expressed in the left quantity's unit.
        public static Quantity operator +(Quantity left, Quantity right) => new Quantity(
                left.Value + right.Unit.ConvertTo(right.Value, left.Unit),
                left.Unit);

        // By convention, the result is expressed in the left quantity's unit.
        public static Quantity operator -(Quantity left, Quantity right) => new Quantity(
                left.Value - right.Unit.ConvertTo(right.Value, left.Unit),
                left.Unit);

        #endregion

        #region Formatting

        public override string ToString() => QuantityFormatter.Format(this, null, null);

        public string ToString(string format) => QuantityFormatter.Format(this, format, null);

        public string ToString(IFormatProvider formatProvider) => QuantityFormatter.Format(this, null, formatProvider);

        public string ToString(string format, IFormatProvider formatProvider) => QuantityFormatter.Format(this, format, formatProvider);

        #endregion

        ////public override string ToString() => $"{Value} {Unit.Symbol}";

        ////public string ToString(string format, IFormatProvider formatProvider)
        ////{
        ////    // TODO!
        ////    return ToString();
        ////}
    }
}
