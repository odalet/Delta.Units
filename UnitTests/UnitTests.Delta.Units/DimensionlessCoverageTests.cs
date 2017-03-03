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
            var degrees = 1m * degree;
            var radians = degrees.ConvertTo(radian);
            Assert.Equal(radians.Value, Helpers.PI / 180m);
        }

        [Fact]
        public void DegreeIsRadianDividedByPIMultipliedBy180()
        {
            var radians = 1m * radian;
            var degrees = radians.ConvertTo(degree);

            // We can't be exact: we are accurate up to 13 decimals by using decimal type.
            Assert.Equal(Math.Round(degrees.Value, 13), Math.Round(180m / Helpers.PI, 13));
        }

        [Fact]
        public void RadianIsTurnMultipliedBy2ByPI()
        {
            var turns = 1m * turn;
            var radians = turns.ConvertTo(radian);
            Assert.Equal(radians.Value, 2m * Helpers.PI);
        }

        [Fact]
        public void TurnIsRadianDividedBy2ByPI()
        {
            var radians = 1m * radian;
            var turns = radians.ConvertTo(turn);
            Assert.Equal(turns.Value, 1m / (2m * Helpers.PI));
        }

        // Proportions

        [Fact]
        public void PercentIsPermilleMultipliedBy10()
        {
            var pc = 1m * percent;
            var pm = pc.ConvertTo(permille);
            Assert.Equal(pm.Value, 10m);
        }

        [Fact]
        public void PermilleIsPercentDividedBy10()
        {
            var pm = 1m * permille;
            var pc = pm.ConvertTo(percent);
            Assert.Equal(pc.Value, 0.1m);
        }

        [Fact]
        public void PercentIsPpmMultipliedBy10000()
        {
            var pc = 1m * percent;
            var ppm = pc.ConvertTo(parts_per_million);
            Assert.Equal(ppm.Value, 10000m);
        }
    }
}
