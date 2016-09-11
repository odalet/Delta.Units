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
                x => x * (Math.PI / 180.0),
                x => x * (180.0 / Math.PI));

            s = new Unit("second", "s", BaseDimensions.Time);
            mn = new Unit("minute", "mn", s, x => x * 60.0, x => x / 60.0);
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
            var valueInRadian = Math.PI / 2.0;
            var expectedValueInDegree = 90.0;
            var actualValueInDegree = Math.Round(radian.ConvertTo(valueInRadian, degree), 2);

            Assert.Equal(expectedValueInDegree, actualValueInDegree);

            var valueInDegree = -90.0;
            var expectedValueInRadian  = Math.Round(Math.PI / -2.0, 2);
            var actualValueInRadian = Math.Round(degree.ConvertTo(valueInDegree, radian), 2);

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
            var degreePerMinute = new Unit("degree / minute", "°/mn", degree / mn);
            var radianPerSecond = new Unit("radian / second", "rad/s", radian / s);

            var valueInDegreePerMinute = 90.0; // 90°/mn -> 1.5°/s -> 0.026179939 rad/s
            var expectedValueInRadianPerSecond = Math.Round(0.026179939, 5);
            var actualValueInRadianPerSecond = Math.Round(radian.ConvertTo(valueInDegreePerMinute, degree), 5);
        }
    }
}
