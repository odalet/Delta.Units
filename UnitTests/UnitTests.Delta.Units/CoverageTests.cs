using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class CoverageTests
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

            Assert.Throws<InvalidOperationException>(() =>
            {
                var incorrect = speed1 * speed2;
            });
        }

        [Fact]
        public void IncompatibleUnitsNoCommonBaseConversions()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var conv = m.ConvertTo(1.0, yd);
            });
        }

        [Fact]
        public void IncompatibleUnitsDifferentDimensionsConversions()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var conv = m.ConvertTo(1.0, s);
            });
        }

        [Fact]
        public void NoneUnitsCanBeConvertedTo()
        {
            Assert.Equal(1.0, Unit.None.ConvertTo(1.0, null));
            Assert.Equal(1.0, Unit.Convert(1.0, null, null));
            Assert.Equal(1.0, Unit.Convert(1.0, Unit.None, Unit.None));
        }

        [Fact]
        public void DivideAndMultiplyBy()
        {
            var speed1 = m / s;
            var speed2 = m.DivideBy(s);
            Assert.Equal(1.0, speed1.ConvertTo(1.0, speed2));

            var foo1 = m * s;
            var foo2 = m.MultiplyBy(s);
            Assert.Equal(1.0, foo1.ConvertTo(1.0, foo2));

            var d1 = (Unit)null / s;
            var d2 = m / (Unit) null;
            var d3 = (Unit) null / (Unit) null;

            var m1 = (Unit)null * s;
            var m2 = m * (Unit)null;
            var m3 = (Unit)null * (Unit)null;

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
            var u2 = ((Unit)null) ^ 2;

            Assert.True(u0.IsNone());
            Assert.True(u1.IsNone());
            Assert.True(u2.IsNone());
        }

        [Fact]
        public void BasedOnNone()
        {
            var u0 = new Unit("u0", "u0", BaseDimensions.None);
            var u1 = new Unit("u1", "u1", u0, x => x * 2.0, x => x / 2.0);
            var u2 = new Unit(null, null, null, x => x * 2.0, x => x / 2.0);

            var value = 16.0;
            Assert.Equal(8.0, u0.ConvertTo(value, u1));
            Assert.Equal(8.0, Unit.None.ConvertTo(value, u2));
        }

        [Fact]
        public void CombinePow0IsConstant()
        {
            var function = Helpers.CombinePow(x => x * 2.0, x => x * 2.0, x => x / 2.0, x => x / 2.0, 0);
            foreach (var d in Enumerable.Range(0, 10).Select(i => i / 10.0))
                Assert.Equal(1.0, function(d));
        }

        [Fact]
        public void CombinePowNullIsIdentity()
        {
            var func = Helpers.CombinePow(null, null, null, null, 1);
            foreach (var d in Enumerable.Range(0, 10).Select(i => i / 10.0))
                Assert.Equal(d, func(d));

            var func_ = Helpers.CombinePow(null, null, null, null, -1);
            foreach (var d in Enumerable.Range(0, 10).Select(i => i / 10.0))
                Assert.Equal(d, func_(d));
        }

        [Fact]
        public void UnitIsNone()
        {
            Assert.True(((Unit)null).IsNone());
            Assert.True(Unit.None.IsNone());

            var u = new Unit("u", "u", BaseDimensions.None);
            Assert.True(u.IsNone());
        }
    }
}
