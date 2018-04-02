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
            Assert.Equal(Helpers.PI / 180m, radians.Value);
        }

        [Fact]
        public void DegreeIsRadianDividedByPIMultipliedBy180()
        {
            var radians = 1m * radian;
            var degrees = radians.ConvertTo(degree);

            // We can't be exact: we are accurate up to 13 decimals by using decimal type.
            Assert.Equal(Math.Round(180m / Helpers.PI, 13), Math.Round(degrees.Value, 13));
        }

        [Fact]
        public void RadianIsTurnMultipliedBy2ByPI()
        {
            var turns = 1m * turn;
            var radians = turns.ConvertTo(radian);
            Assert.Equal(2m * Helpers.PI, radians.Value);
        }

        [Fact]
        public void TurnIsRadianDividedBy2ByPI()
        {
            var radians = 1m * radian;
            var turns = radians.ConvertTo(turn);
            Assert.Equal(1m / (2m * Helpers.PI), turns.Value);
        }

        // Proportions

        [Fact]
        public void PercentIsPermilleMultipliedBy10()
        {
            var pc = 1m * percent;
            var pm = pc.ConvertTo(permille);
            Assert.Equal(10m, pm.Value);
        }

        [Fact]
        public void PermilleIsPercentDividedBy10()
        {
            var pm = 1m * permille;
            var pc = pm.ConvertTo(percent);
            Assert.Equal(0.1m, pc.Value);
        }

        [Fact]
        public void PercentIsPpmMultipliedBy10000()
        {
            var pc = 1m * percent;
            var ppm = pc.ConvertTo(parts_per_million);
            Assert.Equal(10000m, ppm.Value);
        }
    }
}
