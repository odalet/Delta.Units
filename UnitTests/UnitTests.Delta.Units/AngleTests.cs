using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class AngleTests
    {
        private readonly Unit m;
        private readonly Unit radian;
        private readonly Unit degree;
        private readonly Unit s;
        private readonly Unit mn;

        public AngleTests()
        {
            m = new Unit("metre", "m", BaseDimensions.Length);
            radian = new Unit("radian", "rad", m / m);
            degree = new Unit("degree", "°", radian, 
                x => x * (Helpers.PI / 180m),
                x => x * (180m / Helpers.PI));

            s = new Unit("second", "s", BaseDimensions.Time);
            mn = new Unit("minute", "mn", s, x => x * 60m, x => x / 60m);
        }

        [Fact]
        public void AngleIsNone()
        {
            // Angles are defined as Length / Length (and radian is specifically metre / metre)
            Assert.True(radian.IsNone());
            Assert.True(degree.IsNone());
        }

        [Fact]
        public void Conversions()
        {
            var valueInRadian = Helpers.PI / 2m;
            var expectedValueInDegree = 90m;
            var actualValueInDegree = radian.ConvertTo(valueInRadian, degree);

            Assert.Equal(expectedValueInDegree, actualValueInDegree);

            var valueInDegree = -90m;

            // With decimal type, precision - here - is up to 26 decimal places
            var expectedValueInRadian  = Math.Round(Helpers.PI / -2m, 26);
            var actualValueInRadian = Math.Round(degree.ConvertTo(valueInDegree, radian), 26);

            Assert.Equal(expectedValueInRadian, actualValueInRadian);
        }

        [Fact]
        public void AngularSpeedDimension()
        {
            var degreePerMinute = new Unit("degree / minute", "°/mn", degree / mn);
            Assert.Equal("T^-1", degreePerMinute.Dimension.GetFormulaAsString());
        }

        [Fact]
        public void AngularSpeedConversion()
        {
            // TOTEST
            var degreePerMinute = new Unit("degree / minute", "°/mn", degree / mn);
            var radianPerSecond = new Unit("radian / second", "rad/s", radian / s);

            var valueInDegreePerMinute = 90m; // 90°/mn -> 1.5°/s -> 0.026179939 rad/s
            var expectedValueInRadianPerSecond = 0.0261799387799149436538553616m;
            var actualValueInRadianPerSecond = degreePerMinute.ConvertTo(valueInDegreePerMinute, radianPerSecond);
            Assert.Equal(expectedValueInRadianPerSecond, actualValueInRadianPerSecond);
        }
    }
}
