using System;

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

        public override string ToString() => $"{Value} {Unit.Symbol}";

        public string ToString(string format, IFormatProvider formatProvider)
        {
            // TODO!
            return ToString();
        }
    }
}
