using System;

namespace Delta.Units.Systems
{
    /// <summary>
    /// Represents an SI Prefix (see https://en.wikipedia.org/wiki/Metric_prefix)
    /// </summary>
    public sealed class SIPrefix
    {
        internal SIPrefix(string name, string symbol, int tenExponent)
        {
            Name = name;
            Symbol = symbol;
            TenExponent = tenExponent;
        }

        public string Name { get; }
        public string Symbol { get; }
        public int TenExponent { get; }

        public double Factor => Math.Pow(10.0, TenExponent);

        public static Unit operator *(SIPrefix prefix, Unit unit) => unit.IsNone() ?
            Unit.None :
            new Unit(prefix.Name + unit.Name, prefix.Symbol + unit.Symbol, unit, prefix.Factor);
    }
}
