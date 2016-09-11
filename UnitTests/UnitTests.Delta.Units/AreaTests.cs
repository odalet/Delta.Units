using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class AreaTests
    {
        private readonly Unit m;
        private readonly Unit dm;
        private readonly Unit cm;

        private readonly Unit m2;
        private readonly Unit dm2;
        private readonly Unit cm2;

        public AreaTests()
        {
            m = new Unit("metre", "m", BaseDimensions.Length);
            dm = new Unit("decimetre", "dm", m, x => 0.1 * x, x => 10.0 * x);
            cm = new Unit("centimetre", "cm", dm, x => 0.1 * x, x => 10.0 * x);

            m2 = new Unit("square metre", "m²", m * m);
            dm2 = dm ^ 2;
            cm2 = cm * cm;
        }

        [Fact]
        public void Compatibility()
        {
            Assert.True(cm.IsCompatibleWith(dm));
            Assert.True(dm.IsCompatibleWith(cm));
            Assert.True(Unit.AreCompatible(cm, dm));
            Assert.True(Unit.AreCompatible(dm, cm));
        }

        [Fact]
        public void Conversions()
        {
            Assert.Equal(100.0, Math.Round(m2.ConvertTo(1.0, dm2), 0));
            Assert.Equal(10000.0, Math.Round(m2.ConvertTo(1.0, cm2), 0));
            Assert.Equal(100.0, Math.Round(dm2.ConvertTo(1.0, cm2), 0));

            Assert.Equal(2.0, Math.Round(cm2.ConvertTo(20000.0, m2), 0));
            Assert.Equal(2.0, Math.Round(dm2.ConvertTo(200.0, m2), 0));
            Assert.Equal(2.0, Math.Round(cm2.ConvertTo(200.0, dm2), 0));
        }
    }
}
