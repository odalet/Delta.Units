using System;
using Delta.Units.Systems;

namespace Delta.Units
{
    public sealed class Unit
    {
        public static Unit None { get; } = new Unit(string.Empty, string.Empty, (Dimension)null);

        // Used to create base units
        public Unit(string name, string symbol, Dimension dimension) :
            this(name, symbol, dimension, true)
        { }

        // Allows to build aliases
        public Unit(string name, string symbol, Unit basedOn) :
            this(name, symbol, basedOn, x => x, x => x)
        { }

        // Allows to build units based on a multiplication factor
        public Unit(string name, string symbol, Unit basedOn, double toBaseUnitFactor) :
            this(name, symbol, basedOn, x => x * toBaseUnitFactor, x => x / toBaseUnitFactor)
        { }

        // Used to create simple derived units (ie units with the same dimension)
        public Unit(string name, string symbol, Unit basedOn,
            Func<double, double> toConversion, Func<double, double> fromConversion) :
            this(name, symbol, basedOn == null ? null : basedOn.Dimension)
        {
            for (var i = 0; i < BaseDimensions.Count; i++)
            {
                var index = i;
                if (Dimension.Formula[index] == 0) continue;
                BaseUnits[index] = (basedOn ?? None).BaseUnits[index];
                FromBase[index] = basedOn == null ? fromConversion : x => fromConversion(basedOn.FromBase[index](x));
                ToBase[index] = basedOn == null ? toConversion : x => basedOn.ToBase[index](toConversion(x));
            }
        }

        // only used internally
        private Unit(string name, string symbol, Dimension dimension, bool generateBaseUnit)
        {
            Name = name ?? string.Empty;
            Symbol = symbol ?? string.Empty;
            Dimension = dimension ?? BaseDimensions.None;

            if (!generateBaseUnit) return;

            // Identity: we are a base unit
            for (var index = 0; index < BaseDimensions.Count; index++)
            {
                if (Dimension.Formula[index] == 0) continue;
                BaseUnits[index] = this;
                FromBase[index] = x => x;
                ToBase[index] = x => x;
            }
        }

        public string Name { get; set; } // The name can be changed
        public string Symbol { get; }
        public Dimension Dimension { get; }

        internal Unit[] BaseUnits { get; } = new Unit[BaseDimensions.Count];
        internal Func<double, double>[] ToBase { get; } = new Func<double, double>[BaseDimensions.Count];
        internal Func<double, double>[] FromBase { get; } = new Func<double, double>[BaseDimensions.Count];

        #region Operations on Units

        public Unit Pow(int exponent)
        {
            var newUnit = new Unit(
                this.IsNone() ? string.Empty : $"{Name} ^ {exponent}",
                this.IsNone() ? string.Empty : $"{Symbol}^{exponent}",
                Dimension.Pow(exponent), false);

            CombineUnitsInto(this, this, newUnit);
            return newUnit;
        }

        public Unit MultiplyBy(Unit other) => this * other;
        public Unit DivideBy(Unit other) => this / other;

        #endregion

        #region Operators overloads

        // Beware, ^ priority is not the mathematical one! It should always be used with parens to avoid mistakes
        // This is because, natively, ^ is the XOR operator, not the pow one... I miss an ** operator in C# 
        public static Unit operator ^(Unit unit, int exponent) => (unit ?? None).Pow(exponent);
        public static Unit operator *(Unit left, Unit right) => Multiply(left ?? None, right ?? None);
        public static Unit operator /(Unit left, Unit right) => Divide(left ?? None, right ?? None);

        public static Quantity operator *(Unit left, double right) => right * left;
        public static Quantity operator *(double left, Unit right) => new Quantity(left, right);
        
        #endregion

        #region Static Helpers

        public static bool AreCompatible(Unit left, Unit right) =>
            (left ?? Unit.None).Dimension == (right ?? Unit.None).Dimension;

        public static double Convert(double value, Unit from, Unit to)
        {
            var fromUnit = from ?? Unit.None;
            var toUnit = to ?? Unit.None;
            if (!AreCompatible(from, to))
                throw new InvalidOperationException($"Units {fromUnit} and {toUnit} are not compatible");

            // TODO: allow for inter-base units conversions
            if (!AreEqual(fromUnit.BaseUnits, toUnit.BaseUnits))
                throw new InvalidOperationException($"Units {fromUnit} and {toUnit} do not share a common base unit");

            var dim = fromUnit.Dimension;
            var temp = value;
            for (var index = 0; index < BaseDimensions.Count; index++)
            {
                var pow = dim.Formula[index];
                if (pow == 0) continue;

                var convert = Helpers.CombinePow(
                    toUnit.FromBase[index], fromUnit.ToBase[index], // F, G
                    toUnit.ToBase[index], fromUnit.FromBase[index], // F_, G_
                    pow);

                temp = convert(temp);
            }

            return temp;
        }

        private static Unit Multiply(Unit left, Unit right)
        {
            var newUnit = new Unit(
                Helpers.CreateUnitName(left, right, "*"),
                Helpers.CreateUnitSymbol(left, right, "."),
                left.Dimension * right.Dimension, false);

            CombineUnitsInto(left, right, newUnit);
            return newUnit;
        }

        private static Unit Divide(Unit left, Unit right)
        {
            var newUnit = new Unit(
                Helpers.CreateUnitName(left, right, "/"),
                Helpers.CreateUnitSymbol(left, right, "/"),
                left.Dimension / right.Dimension, false);

            CombineUnitsInto(left, right, newUnit);
            return newUnit;
        }

        private static void CombineUnitsInto(Unit left, Unit right, Unit target)
        {
            if (target.Dimension.IsNone()) // special case: we store the Identity in the Z dimension
            {
                target.BaseUnits[DimensionFormulaIndices.z] = target;
                target.FromBase[DimensionFormulaIndices.z] = x => x;
                target.ToBase[DimensionFormulaIndices.z] = x => x;
            }

            for (var index = 0; index < BaseDimensions.Count; index++)
            {
                var ldim = left.Dimension.Formula[index];
                var rdim = right.Dimension.Formula[index];

                var leftFrom = left.FromBase[index];
                var rightFrom = right.FromBase[index];
                var leftTo = left.ToBase[index];
                var rightTo = right.ToBase[index];

                if (ldim != 0 && rdim != 0)
                {
                    // Both units have a component in this dimension; make sure it is the same base unit
                    if (left.BaseUnits[index] != right.BaseUnits[index])
                    {
                        var d = BaseDimensions.All[index].Symbol;
                        throw new InvalidOperationException(
                            $"Incompatible units: could not find a common base unit for dimension '{d}'");
                    }
                    
                    target.BaseUnits[index] = left.BaseUnits[index];
                    // The enclosed 'leftFrom(rightTo(x))' expression below is equivalent 
                    // to converting x from right unit to left unit for this dimension.
                    target.FromBase[index] = x => leftFrom(leftFrom(rightTo(x)));
                    target.ToBase[index] = x => leftTo(leftFrom(rightTo(x)));
                }
                else if (ldim != 0)
                {
                    target.BaseUnits[index] = left.BaseUnits[index];
                    target.FromBase[index] = leftFrom;
                    target.ToBase[index] = leftTo;
                }
                else if (rdim != 0)
                {
                    target.BaseUnits[index] = right.BaseUnits[index];
                    target.FromBase[index] = rightFrom;
                    target.ToBase[index] = rightTo;
                }
            }
        }

        private static bool AreEqual(Unit[] leftBaseUnits, Unit[] rightBaseUnits)
        {
            for (var index = 0; index < BaseDimensions.Count; index++)
            {
                // TODO: Add == & != overloads
                if (leftBaseUnits[index] != rightBaseUnits[index]) return false;
            }

            return true;
        }

        #endregion
    }
}
