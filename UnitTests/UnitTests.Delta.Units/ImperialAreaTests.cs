using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.Imperial;
using static Delta.Units.Systems.SI;

namespace Delta.Units
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
        public void One_rood_is_10_117141056_ares()
        {
            var oneRood = 1 * rood;
            var sqm = oneRood.ConvertTo(metre * metre);
            var ares = oneRood.ConvertTo(Area.are);
            
            Assert.Equal(10.117141056m, ares.Value);
        }

        [Fact]
        public void One_acre_is_0_40468564224_hectares()
        {
            var oneAcre = 1 * acre;
            var hectares = oneAcre.ConvertTo(Area.hectare);

            Assert.Equal(0.40468564224m, hectares.Value);
        }
    }
}
