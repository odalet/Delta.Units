using System;
using Delta.Units.Globalization;

namespace Delta.Units
{
    /// <summary>
    /// A quantity stores a pair consisting of a <see cref="decimal"/> value and a <see cref="Unit"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="Quantity"/> types supports operators overloads that correctly compute the resulting unit and value.
    /// </remarks>
    public sealed class Quantity : IFormattable
    {
        public Quantity(decimal value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public decimal Value { get; }
        public Unit Unit { get; }

        public Quantity ConvertTo(Unit target) => new Quantity(
            Unit.Convert(Value, Unit, target), target);

        #region Operator Overloads

        // double op quantity & quantity op double

        public static Quantity operator +(Quantity left, decimal right) => right + left;
        public static Quantity operator +(decimal left, Quantity right) => new Quantity(left + right.Value, right.Unit);

        public static Quantity operator -(Quantity left, decimal right) => (-1m * right) + left;
        public static Quantity operator -(decimal left, Quantity right) => (-1m * left) + right;

        public static Quantity operator *(Quantity left, decimal right) => right * left;
        public static Quantity operator *(decimal left, Quantity right) => new Quantity(left * right.Value, right.Unit);

        public static Quantity operator /(Quantity left, decimal right) => (1m / right) * left;
        public static Quantity operator /(decimal left, Quantity right) => (1m / left) * right;

        // double-based overloads are defined so that it is easy for the user to define quantities.
        // Ho<ever beware of the precision and floating-point rounding issues

        public static Quantity operator +(Quantity left, double right) => left + (decimal)right;
        public static Quantity operator +(double left, Quantity right) => (decimal)left + right;

        public static Quantity operator -(Quantity left, double right) => left - (decimal)right;
        public static Quantity operator -(double left, Quantity right) => (decimal)left - right;

        public static Quantity operator *(Quantity left, double right) => left * (decimal)right;
        public static Quantity operator *(double left, Quantity right) => (decimal)left * right;

        public static Quantity operator /(Quantity left, double right) => left / (decimal)right;
        public static Quantity operator /(double left, Quantity right) => (decimal)left / right;

        // Because Int32 can be implicitely converted to double or decimal, we need also provide
        // overloads for disambiguation.
        // By the way such disambiguation should also be necessary for other integer types, but 
        // we'll leave it to the user to explicitely cast to int, decimal or double.
        public static Quantity operator +(Quantity left, int right) => left + (decimal)right;
        public static Quantity operator +(int left, Quantity right) => (decimal)left + right;

        public static Quantity operator -(Quantity left, int right) => left - (decimal)right;
        public static Quantity operator -(int left, Quantity right) => (decimal)left - right;

        public static Quantity operator *(Quantity left, int right) => left * (decimal)right;
        public static Quantity operator *(int left, Quantity right) => (decimal)left * right;

        public static Quantity operator /(Quantity left, int right) => left / (decimal)right;
        public static Quantity operator /(int left, Quantity right) => (decimal)left / right;

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
    }
}
