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
            dm = new Unit("decimetre", "dm", m, x => 0.1m * x, x => 10m * x);
            cm = new Unit("centimetre", "cm", dm, x => 0.1m * x, x => 10m * x);

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
            Assert.Equal(100m, m2.ConvertTo(1m, dm2));
            Assert.Equal(10000m, m2.ConvertTo(1m, cm2));
            Assert.Equal(100m, dm2.ConvertTo(1m, cm2));

            Assert.Equal(2m, cm2.ConvertTo(20000m, m2));
            Assert.Equal(2m, dm2.ConvertTo(200m, m2));
            Assert.Equal(2m, cm2.ConvertTo(200m, dm2));
        }
    }
}
