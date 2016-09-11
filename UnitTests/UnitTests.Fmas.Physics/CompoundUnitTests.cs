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

            km = new Unit("kilometre", "km", m, x => x * 1000.0, x => x / 1000.0);
            h = new Unit("hour", "h", s, x => x * 3600.0, x => x / 3600.0);
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
            var valueInMps = 1.1; // 1.1 m/s
            var expectedValueInKmph = 3.96; // 3.96 km/h
            var actualValueInKmph = Math.Round(mps.ConvertTo(valueInMps, kmph), 2);
            Assert.Equal(expectedValueInKmph, actualValueInKmph);

            var valueInKmps = 4.4;
            var expectedValueInMps = 1.22222;
            var actualValueInMps = Math.Round(kmph.ConvertTo(valueInKmps, mps), 5);
            Assert.Equal(expectedValueInMps, actualValueInMps);
        }


        [Fact]
        public void ComplexConversions()
        {
            var mile = new Unit("mile", "mile", km, x => 1.609344 * x, x => x / 1.609344);
            var minute = new Unit("minute", "mn", s, x => x * 60.0, x => x / 60.0);
            var minuteEx = new Unit("minuteEx", "mnEx", h, x => x / 60.0, x => x * 60.0);

            var milePerMinute = new Unit("mile per minute", "mile/mn", mile / minute);
            var milePerMinuteEx = new Unit("mile per minuteEx", "mile/mnEx", mile / minuteEx);

            var valueInKmph = 100.0; // This is 62.1371 mph

            // We must round to 5 decimals: there is quite a precision loss (due to the nested lambdas?) 
            // and also, I'm not that sure of the conversion factor I used for miles to km
            var r = 5;
            var expectedValueInMilePerMinute = Math.Round(1.035618333, r); 
            var expectedValueInMilePerMinuteEx = expectedValueInMilePerMinute;

            var actualValueInMilePerMinute = Math.Round(kmph.ConvertTo(valueInKmph, milePerMinute), r);
            var actualValueInMilePerMinuteEx = Math.Round(kmph.ConvertTo(valueInKmph, milePerMinuteEx), r);

            Assert.Equal(expectedValueInMilePerMinute, actualValueInMilePerMinute);
            Assert.Equal(expectedValueInMilePerMinuteEx, actualValueInMilePerMinuteEx);
        }
    }
}
