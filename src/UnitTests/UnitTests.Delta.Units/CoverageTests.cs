using System;
using System.Diagnostics.CodeAnalysis;
using Delta.Units.Systems;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public partial class CoverageTests
    {
        private readonly Unit s;
        private readonly Unit m;
        private readonly Unit yd;

        public CoverageTests()
        {
            s = new Unit("second", "s", BaseDimensions.Time);
            m = new Unit("metre", "m", BaseDimensions.Length);
            yd = new Unit("yard", "yd", BaseDimensions.Length);
        }

        [Fact]
        public void IncompatibleUnitsCombination()
        {
            var speed1 = m / s;
            var speed2 = yd / s;

            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                var incorrect = speed1 * speed2;
            });
        }

        [Fact]
        public void IncompatibleUnitsNoCommonBaseConversions() =>
            Assert.Throws<InvalidOperationException>(() => _ = m.ConvertTo(1m, yd));

        [Fact]
        public void IncompatibleUnitsDifferentDimensionsConversions() =>
            Assert.Throws<InvalidOperationException>(() => _ = m.ConvertTo(1m, s));

        [Fact]
        public void NoneUnitsCanBeConvertedTo()
        {
            Assert.Equal(1m, Unit.None.ConvertTo(1m, null));
            Assert.Equal(1m, Unit.Convert(1m, null, null));
            Assert.Equal(1m, Unit.Convert(1m, Unit.None, Unit.None));
        }

        [Fact]
        public void DivideAndMultiplyBy()
        {
            var speed1 = m / s;
            var speed2 = m.DivideBy(s);
            Assert.Equal(1m, speed1.ConvertTo(1m, speed2));

            var foo1 = m * s;
            var foo2 = m.MultiplyBy(s);
            Assert.Equal(1m, foo1.ConvertTo(1m, foo2));

            var d1 = null / s;
            var d2 = m / null;
            var d3 = (Unit)null / null;

            var m1 = null * s;
            var m2 = m * null;
            var m3 = (Unit)null * null;

            Assert.Equal("T^-1", d1.Dimension.GetFormulaAsString());
            Assert.Equal("L", d2.Dimension.GetFormulaAsString());
            Assert.Equal("Z", d3.Dimension.GetFormulaAsString());
            Assert.Equal("T", m1.Dimension.GetFormulaAsString());
            Assert.Equal("L", m2.Dimension.GetFormulaAsString());
            Assert.Equal("Z", m3.Dimension.GetFormulaAsString());
        }

        [Fact]
        public void NonePow()
        {
            var u0 = Unit.None ^ 0;
            var u1 = Unit.None ^ 1;
            var u_1 = Unit.None ^ -1;
            var u2 = ((Unit)null) ^ 2;

            Assert.True(u0.IsNone());
            Assert.True(u1.IsNone());
            Assert.True(u_1.IsNone());
            Assert.True(u2.IsNone());
        }

        [Fact]
        public void BasedOnNone()
        {
            var u0 = new Unit("u0", "u0", BaseDimensions.None);
            var u1 = new Unit("u1", "u1", u0, x => x * 2m, x => x / 2m);
            var u2 = new Unit(null, null, null, x => x * 2m, x => x / 2m);

            var value = 16m;
            Assert.Equal(8m, u0.ConvertTo(value, u1));
            Assert.Equal(8m, Unit.None.ConvertTo(value, u2));
        }

        [Fact]
        public void UnitIsNone()
        {
            Assert.True(((Unit)null).IsNone());
            Assert.True(Unit.None.IsNone());

            var u = new Unit("u", "u", BaseDimensions.None);
            Assert.True(u.IsNone());
        }

        [Fact]
        public void NoneUnitWithPrefixIsNoneUnit()
        {
            var none = (Unit)null;
            var millinone = SI.Prefixes.milli * none;
            Assert.True(millinone.IsNone());
        }

        [Fact]
        public void UnitsAndDecimalMultiplicationIsCommutative()
        {
            var q1 = 42m * SI.metre;
            var q2 = SI.metre * 42m;

            Assert.Equal(q1.Value, q2.Value);
            Assert.Equal(q1.Unit.Name, q2.Unit.Name);
        }

        [Fact]
        public void UnitsAndDoubleMultiplicationIsCommutative()
        {
            var q1 = 42.0 * SI.metre;
            var q2 = SI.metre * 42.0;

            Assert.Equal(q1.Value, q2.Value);
            Assert.Equal(q1.Unit.Name, q2.Unit.Name);
        }

        [Fact]
        public void UnitsAndIntMultiplicationIsCommutative()
        {
            var q1 = 42 * SI.metre;
            var q2 = SI.metre * 42;

            Assert.Equal(q1.Value, q2.Value);
            Assert.Equal(q1.Unit.Name, q2.Unit.Name);
        }
    }
}
