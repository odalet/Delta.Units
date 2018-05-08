using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.SI;
using static Delta.Units.Systems.Imperial;

namespace UnitTests.Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class ImperialAreaTests
    {
        [Fact]
        public void One_perch_is_25_29285264_square_metre()
        {
            var onePerch = 1 * perch;
            var sqm = onePerch.ConvertTo(metre * metre);

            Assert.Equal(25.29285264m, sqm.Value);
        }

        [Fact]
        public void One_rood_is_10_12_ares()
        {
            var oneRood = 1 * rood;
            var sqm = oneRood.ConvertTo(metre * metre);
            var ares = oneRood.ConvertTo(Area.are);

            Assert.Equal(10.12m, ares.Value);
        }

        [Fact]
        public void One_acre_is_0_40468564224_hectares()
        {
            var oneFm = (1 * furlong).ConvertTo(metre);
            var oneCm= (1 * chain).ConvertTo(metre);
            var mm1 = oneCm * oneFm;

            var oneFC = (1 * furlong) * (1 * chain);
            var mm2 = oneFC.ConvertTo(metre * metre);

            var mm = (1 * (furlong * chain)).ConvertTo(metre * metre);


            var oneAcre = 1 * acre;
            var sqm = oneAcre.ConvertTo(metre * metre);
            var hectares = oneAcre.ConvertTo(Area.hectare);

            Assert.Equal(0.4047m, hectares.Value);
        }
    }
}
