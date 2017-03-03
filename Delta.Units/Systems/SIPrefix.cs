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

        public decimal Factor => Pow10(TenExponent);

        public static Unit operator *(SIPrefix prefix, Unit unit) => unit.IsNone() ?
            Unit.None :
            new Unit(prefix.Name + unit.Name, prefix.Symbol + unit.Symbol, unit, prefix.Factor);
        
        private static decimal Pow10(int tenExponent)
        {
            var absExponent = tenExponent > 0 ? tenExponent : -tenExponent;
            var initial = 1m;
            for (int i = 0; i < absExponent; i++)
                initial = tenExponent > 0 ? initial * 10m : initial / 10m;
            return initial;
        }
    }
}
