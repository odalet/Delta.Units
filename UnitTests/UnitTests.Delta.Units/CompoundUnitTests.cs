using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class CompoundUnitTests
    {
        private readonly Unit m;
        private readonly Unit s;
        private readonly Unit mps;

        private readonly Unit km;
        private readonly Unit h;
        private readonly Unit kmph;
        private readonly Unit none = Unit.None;

        public CompoundUnitTests()
        {
            m = new Unit("metre", "m", BaseDimensions.Length);
            s = new Unit("second", "s", BaseDimensions.Time);
            mps = m / s;

            km = new Unit("kilometre", "km", m, x => x * 1000m, x => x / 1000m);
            h = new Unit("hour", "h", s, x => x * 3600m, x => x / 3600m);
            kmph = km / h;
        }

        [Fact]
        public void Compatibility()
        {
            Assert.True(kmph.IsCompatibleWith(mps));
            Assert.True(mps.IsCompatibleWith(kmph));
            Assert.True(Unit.AreCompatible(kmph, mps));
            Assert.True(Unit.AreCompatible(mps, kmph));
        }

        [Fact]
        public void CompatibilityWithNone()
        {
            var nil = (Unit)null;

            Assert.False(kmph.IsCompatibleWith(none));
            Assert.False(kmph.IsCompatibleWith(nil));
            Assert.False(Unit.AreCompatible(kmph, none));
            Assert.False(Unit.AreCompatible(kmph, nil));
        }

        [Fact]
        public void SimpleConversions()
        { 
            var valueInMps = 1.1m; // 1.1 m/s
            var expectedValueInKmph = 3.96m; // 3.96 km/h
            var actualValueInKmph = mps.ConvertTo(valueInMps, kmph);
            Assert.Equal(expectedValueInKmph, actualValueInKmph);

            var valueInKmps = 4.4m;
            var expectedValueInMps = 1.22222m; // 1.22222...
            var actualValueInMps = Math.Round(kmph.ConvertTo(valueInKmps, mps), 5);
            Assert.Equal(expectedValueInMps, actualValueInMps);
        }

        [Fact]
        public void ComplexConversions()
        {
            //TOTEST
            var mile = new Unit("mile", "mile", km, x => 1.609344m * x, x => x / 1.609344m);
            var minute = new Unit("minute", "mn", s, x => x * 60m, x => x / 60m);
            var minuteEx = new Unit("minuteEx", "mnEx", h, x => x / 60m, x => x * 60m);

            var milePerMinute = new Unit("mile per minute", "mile/mn", mile / minute);
            var milePerMinuteEx = new Unit("mile per minuteEx", "mile/mnEx", mile / minuteEx);

            var valueInKmph = 100m; // This is 62.1371 mph --> 1.035618333 miles/mn

            var valueInMilePerMinute = kmph.ConvertTo(valueInKmph, milePerMinute);
            var valueInMilePerMinuteEx = kmph.ConvertTo(valueInKmph, milePerMinuteEx);

            Assert.Equal(valueInMilePerMinute, valueInMilePerMinuteEx);
        }
    }
}
