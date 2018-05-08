using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class ProportionTests
    {
        private readonly Unit u;
        private readonly Unit percent;
        private readonly Unit permille;
        private readonly Unit ppm;

        public ProportionTests()
        {
            u = new Unit("unit", "u", BaseDimensions.AmountOfSubstance / BaseDimensions.AmountOfSubstance);
            percent = new Unit("percent", "%", u, x => x / 100m, x => x * 100m);
            permille = new Unit("permille", SpecialCharacters.permille, percent, x => x / 10m, x => x * 10m);
            ppm = new Unit("part per million", "ppm", permille, x => x / 1000m, x => x * 1000m);
        }

        [Fact]
        public void ProportionIsNone()
        {
            // proportions are defined as Amount / Amount
            Assert.True(u.IsNone());
            Assert.True(percent.IsNone());
            Assert.True(permille.IsNone());
            Assert.True(ppm.IsNone());
        }

        [Fact]
        public void Conversions()
        {
            var valueInUnit = 3.5m;
            var expectedValueInPercent = 350m;
            var expectedValueInPermille = 3500m;
            var expectedValueInPpm = 3500000m;
            var actualValueInPercent = u.ConvertTo(valueInUnit, percent);
            var actualValueInPermille = u.ConvertTo(valueInUnit, permille);
            var actualValueInPpm = u.ConvertTo(valueInUnit, ppm);

            Assert.Equal(expectedValueInPercent, actualValueInPercent);
            Assert.Equal(expectedValueInPermille, actualValueInPermille);
            Assert.Equal(expectedValueInPpm, actualValueInPpm);
        }
    }
}
