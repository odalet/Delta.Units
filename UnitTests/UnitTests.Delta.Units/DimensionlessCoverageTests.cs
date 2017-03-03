using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.Dimensionless;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class DimensionlessCoverageTests
    {        
        // Angles

        [Fact]
        public void RadianIsDegreeMultipliedByPIDividedBy180()
        {
            var degrees = 1.0 * degree;
            var radians = degrees.ConvertTo(radian);
            Assert.Equal(radians.Value, Math.PI / 180.0);
        }

        [Fact]
        public void DegreeIsRadianDividedByPIMultipliedBy180()
        {
            var radians = 1.0 * radian;
            var degrees = radians.ConvertTo(degree);
            Assert.Equal(degrees.Value, 180.0 / Math.PI);
        }

        [Fact]
        public void RadianIsTurnMultipliedBy2ByPI()
        {
            var turns = 1.0 * turn;
            var radians = turns.ConvertTo(radian);
            Assert.Equal(radians.Value, 2.0 * Math.PI);
        }

        [Fact]
        public void TurnIsRadianDividedBy2ByPI()
        {
            var radians = 1.0 * radian;
            var turns = radians.ConvertTo(turn);
            Assert.Equal(turns.Value, 1.0 / (2.0 * Math.PI));
        }

        // Proportions

        [Fact]
        public void PercentIsPermilleMultipliedBy10()
        {
            var pc = 1.0 * percent;
            var pm = pc.ConvertTo(permille);
            Assert.Equal(pm.Value, 10.0);
        }

        [Fact]
        public void PermilleIsPercentDividedBy10()
        {
            var pm = 1.0 * permille;
            var pc = pm.ConvertTo(percent);
            Assert.Equal(pc.Value, 0.1);
        }

        [Fact]
        public void PercentIsPpmMultipliedBy10000()
        {
            var pc = 1.0 * percent;
            var ppm = pc.ConvertTo(parts_per_million);
            Assert.Equal(ppm.Value, 10000.0);
        }
    }
}
