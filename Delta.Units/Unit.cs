using System;
using System.Globalization;
using Delta.Units.Globalization;

namespace Delta.Units
{
    /// <summary>
    /// This is a unit of measure. 
    /// </summary>
    /// <seealso cref="System.IFormattable" />
    public sealed class Unit : IFormattable
    {
        private class SpecificUnitTranslationProvider : IUnitTranslationProvider
        {
            public SpecificUnitTranslationProvider(Unit unit)
            {
                Unit = unit;
            }

            private Unit Unit { get; }

            public string TranslateName(Unit _, CultureInfo culture)
            {
                // In this method, we do not care what unit is passed in, we always only translate the Unit that we were created by.
                if (Unit.TranslateNameFunction == null) return DefaultUnitTranslationProvider.Current.TranslateName(Unit, culture);
                try
                {
                    return Unit.TranslateNameFunction(culture);
                }
                catch
                {
                    return DefaultUnitTranslationProvider.Current.TranslateName(Unit, culture);
                }
            }

            public string TranslateSymbol(Unit _, CultureInfo culture)
            {
                // In this method, we do not care what unit is passed in, we always only translate the Unit that we were created by.
                if (Unit.TranslateSymbolFunction == null) return DefaultUnitTranslationProvider.Current.TranslateSymbol(Unit, culture);
                try
                {
                    return Unit.TranslateSymbolFunction(culture);
                }
                catch
                {
                    return DefaultUnitTranslationProvider.Current.TranslateSymbol(Unit, culture);
                }
            }
        }

        /// <summary>
        /// Gets the 'none' unit used to express quantities with no physical unit (such as ratios).
        /// </summary>
        public static Unit None { get; } = new Unit(string.Empty, string.Empty, (Dimension)null);

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class from a name, symbol and dimension.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="dimension">The dimension.</param>
        public Unit(string name, string symbol, Dimension dimension) : this(name, symbol, dimension, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class from a name, symbol and another <see cref="Unit"/>.
        /// </summary>
        /// <remarks>
        /// This constructor allows to create unit aliases
        /// </remarks>
        /// <param name="name">The name.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="basedOn">The unit this unit is based on.</param>
        public Unit(string name, string symbol, Unit basedOn) : this(name, symbol, basedOn, x => x, x => x) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class from a name, symbol, another <see cref="Unit"/> and the multiplication factor
        /// allowing to convert the new unit to the specified base unit.
        /// </summary>
        /// <remarks>
        /// This constructor allows to create units realated by a factor.
        /// </remarks>
        /// <param name="name">The name.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="basedOn">The unit this unit is based on.</param>
        /// <param name="toBaseUnitFactor">The factor that, when applied to a quantity expressed in this unit, converts it to the base unit.</param>
        public Unit(string name, string symbol, Unit basedOn, decimal toBaseUnitFactor) :
            this(name, symbol, basedOn, x => x * toBaseUnitFactor, x => x / toBaseUnitFactor)
        { }

        // Used to create simple derived units (ie units with the same dimension)
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class from a name, symbol, another <see cref="Unit"/> and then,
        /// the function that converts from this unit to the base unit and the inverse function.
        /// </summary>
        /// <remarks>
        /// This constructor allows to create units realated by a factor.
        /// </remarks>
        /// <param name="name">The name.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="basedOn">The unit this unit is based on.</param>
        /// <param name="convertToBaseUnit">The function that, when applied to a quantity expressed in this unit, converts it to the base unit.</param>
        /// <param name="convertFromBaseUnit">The function that, when applied to a quantity expressed in the base unit, converts it to this unit.</param>
        public Unit(string name, string symbol, Unit basedOn,
            Func<decimal, decimal> convertToBaseUnit, Func<decimal, decimal> convertFromBaseUnit) :
            this(name, symbol, basedOn?.Dimension)
        {
            for (var i = 0; i < BaseDimensions.Count; i++)
            {
                var index = i;
                if (Dimension.Formula[index] == 0) continue;
                BaseUnits[index] = (basedOn ?? None).BaseUnits[index];
                FromBase[index] = basedOn == null ? convertFromBaseUnit : x => convertFromBaseUnit(basedOn.FromBase[index](x));
                ToBase[index] = basedOn == null ? convertToBaseUnit : x => basedOn.ToBase[index](convertToBaseUnit(x));
            }
        }

        // only used internally
        private Unit(string name, string symbol, Dimension dimension, bool generateBaseUnit)
        {
            Name = name ?? string.Empty;
            Symbol = symbol ?? string.Empty;
            Dimension = dimension ?? BaseDimensions.None;
            TranslationProvider = new SpecificUnitTranslationProvider(this);

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

        /// <summary>
        /// Gets or sets this unit name.
        /// </summary>
        public string Name { get; set; } // The name can be changed

        /// <summary>
        /// Gets this unit symbol.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Gets this unit's dimension formula.
        /// </summary>
        public Dimension Dimension { get; }

        /// <summary>
        /// Gets or sets a function that can translate the unit name into another culture.
        /// </summary>
        public Func<CultureInfo, string> TranslateNameFunction { get; set; }

        /// <summary>
        /// Gets or sets a function that can translate the unit symbol into another culture.
        /// </summary>
        public Func<CultureInfo, string> TranslateSymbolFunction { get; set; }

        /// <summary>
        /// Gets a translation provider for this unit (<see cref="IUnitTranslationProvider"/>).
        /// </summary>
        public IUnitTranslationProvider TranslationProvider { get; }

        internal Unit[] BaseUnits { get; } = new Unit[BaseDimensions.Count];
        internal Func<decimal, decimal>[] ToBase { get; } = new Func<decimal, decimal>[BaseDimensions.Count];
        internal Func<decimal, decimal>[] FromBase { get; } = new Func<decimal, decimal>[BaseDimensions.Count];

        #region Operations on Units

        /// <summary>
        /// Creates a new <see cref="Unit"/> that is this unit multiplied <paramref name="exponent"/> times by itself
        /// </summary>
        /// <param name="exponent">The exponent.</param>
        /// <returns>A new <see cref="Unit"/>.</returns>
        public Unit Pow(int exponent)
        {
            var newUnit = new Unit(
                this.IsNone() ? string.Empty : $"{Name} ^ {exponent}",
                this.IsNone() ? string.Empty : $"{Symbol}^{exponent}",
                Dimension.Pow(exponent), false);

            CombineUnitsInto(this, this, newUnit);
            return newUnit;
        }

        /// <summary>
        /// Multiplies this unit by the specified <paramref name="other"/> unit.
        /// </summary>
        /// <param name="other">The other unit.</param>
        /// <returns>A new <see cref="Unit"/>.</returns>
        public Unit MultiplyBy(Unit other) => this * other;

        /// <summary>
        /// Divides this unit by the specified <paramref name="other"/> unit.
        /// </summary>
        /// <param name="other">The other unit.</param>
        /// <returns>A new <see cref="Unit"/>.</returns>
        public Unit DivideBy(Unit other) => this / other;

        #endregion

        #region Operators overloads

        // Beware, ^ priority is not the mathematical one! It should always be used with parens to avoid mistakes
        // This is because, natively, ^ is the XOR operator, not the pow one... I miss an ** operator in C# 

        /// <summary>
        /// Implements the operator ^ on a unit and an integer exponent. NB: always enclose these expressions in parentheses.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Unit operator ^(Unit unit, int exponent) => (unit ?? None).Pow(exponent);

        /// <summary>
        /// Implements the operator * on two units.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Unit operator *(Unit left, Unit right) => Multiply(left ?? None, right ?? None);

        /// <summary>
        /// Implements the operator / on two units.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Unit operator /(Unit left, Unit right) => Divide(left ?? None, right ?? None);

        /// <summary>
        /// Implements the operator * on a unit and a <see cref="decimal"/> number.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator *(Unit left, decimal right) => right * left;

        /// <summary>
        /// Implements the operator * on a <see cref="decimal"/> number and a unit.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator *(decimal left, Unit right) => new Quantity(left, right);

        // double-based overloads are defined so that it is easy for the user to define quantities.
        // Ho<ever beware of the precision and floating-point rounding issues

        /// <summary>
        /// Implements the operator * on a unit and a <see cref="double"/> number. 
        /// NB: internally the <see cref="double"/> operand is converted to <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator *(Unit left, double right) => left * (decimal)right;

        /// <summary>
        /// Implements the operator * on a <see cref="double"/> number and a unit. 
        /// NB: internally the <see cref="double"/> operand is converted to <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator *(double left, Unit right) => (decimal)left * right;

        // Because Int32 can be implicitely converted to double or decimal, we need also provide
        // overloads for disambiguation.
        // By the way such disambiguation should also be necessary for other integer types, but 
        // we'll leave it to the user to explicitely cast to int, decimal or double.

        /// <summary>
        /// Implements the operator * on a unit and a <see cref="int"/> number. 
        /// NB: internally the <see cref="int"/> operand is converted to <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator *(Unit left, int right) => left * (decimal)right;

        /// <summary>
        /// Implements the operator * on a <see cref="int"/> number and a unit. 
        /// NB: internally the <see cref="int"/> operand is converted to <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Quantity operator *(int left, Unit right) => (decimal)left * right;

        #endregion

        #region Formatting

        /// <inheritdoc />
        public override string ToString() => UnitFormatter.Format(this, null, null);

        /// <inheritdoc />
        public string ToString(string format) => UnitFormatter.Format(this, format, null);

        /// <inheritdoc />
        public string ToString(IFormatProvider formatProvider) => UnitFormatter.Format(this, null, formatProvider);

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider) => UnitFormatter.Format(this, format, formatProvider);

        #endregion

        #region Static Helpers

        /// <summary>
        /// Determines whether the specified units are compatible (i.e. whether theay belong to the same <see cref="Dimension"/>).
        /// </summary>
        /// <param name="left">The first unit to compare.</param>
        /// <param name="right">The second unit to compare.</param>
        /// <returns><c>true</c> if the units are compatible; otherwise, <c>false</c>.</returns>
        public static bool AreCompatible(Unit left, Unit right) => (left ?? None).Dimension == (right ?? None).Dimension;

        /// <summary>
        /// Converts the specified <paramref name="value"/> expressed in the <paramref name="from"/> unit to a value expressed in the <paramref name="to"/> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="from">The value's original unit.</param>
        /// <param name="to">The converted value's unit.</param>
        /// <returns>A value equivalent to <paramref name="value"/>, but expressed using <paramref name="to"/> unit.</returns>
        public static decimal Convert(decimal value, Unit from, Unit to)
        {
            var fromUnit = from ?? None;
            var toUnit = to ?? None;
            if (!AreCompatible(from, to))
                throw new InvalidOperationException($"Units {fromUnit} and {toUnit} are not compatible");

            // TODO: allow for inter-base units conversions
            if (!AreEqual(fromUnit.BaseUnits, toUnit.BaseUnits))
                throw new InvalidOperationException($"Units {fromUnit} and {toUnit} do not share a common base unit");

            if (fromUnit.Dimension != toUnit.Dimension)
                throw new InvalidOperationException($"Units {fromUnit} and {toUnit} do not share the same dimension formula");

            var dim = fromUnit.Dimension;
            var temp = value;
            for (var index = 0; index < BaseDimensions.Count; index++)
            {
                var pow = dim.Formula[index];
                if (pow == 0) continue;

                // We only look, at the sign of pow (not its value) so as to determine the direction of the conversion
                // The value of the pow is already part of the conversion function (see CombineUnitsInto below)
                temp = pow > 0 ?
                    toUnit.FromBase[index](fromUnit.ToBase[index](temp)) :
                    toUnit.ToBase[index](fromUnit.FromBase[index](temp));
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
                var lpow = left.Dimension.Formula[index];
                var rpow = right.Dimension.Formula[index];

                var lfrom = left.FromBase[index]; // base -> left
                var rfrom = right.FromBase[index]; // base -> right
                var lto = left.ToBase[index]; // left -> base
                var rto = right.ToBase[index]; // right -> base

                if (lpow != 0 && rpow != 0)
                {
                    // Both units have a component in this dimension; make sure it is the same base unit
                    if (left.BaseUnits[index] != right.BaseUnits[index])
                    {
                        var d = BaseDimensions.All[index].Symbol;
                        throw new InvalidOperationException(
                            $"Incompatible units: could not find a common base unit for dimension '{d}'");
                    }

                    target.BaseUnits[index] = left.BaseUnits[index]; // == right.BaseUnits[index]

                    Func<decimal, decimal> rightToLeft = x => lfrom(rto(x)); // This converts from the right to the left unit
                    Func<decimal, decimal> leftToRight = x => rfrom(lto(x)); // This converts from the left to the right unit

                    // The pow associated with target's base unit in the current dimension
                    var pow = lpow + rpow;

                    // new unit -> base^pow unit is: ((left -> base) ° (right -> left)) ^ pow
                    target.ToBase[index] = Helpers.CombinePow(lto, rightToLeft, lfrom, leftToRight, pow);

                    // new base^pow unit -> new unit is: ((right -> left) ° (base -> left)) ^ pow
                    target.FromBase[index] = Helpers.CombinePow(lfrom, rightToLeft, lto, leftToRight, pow);
                }
                else if (lpow != 0)
                {
                    target.BaseUnits[index] = left.BaseUnits[index];
                    target.FromBase[index] = lfrom;
                    target.ToBase[index] = lto;
                }
                else if (rpow != 0)
                {
                    target.BaseUnits[index] = right.BaseUnits[index];
                    target.FromBase[index] = rfrom;
                    target.ToBase[index] = rto;
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
