using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.SI;
using static Delta.Units.Systems.Imperial;

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

        [Fact]
        public void foo()
        {
            // TODO
            var oneFC = (1 * furlong) * (1 * chain);
            var mm2 = oneFC.ConvertTo(metre * metre);

            var _mm = (1 * (furlong * chain)).ConvertTo(metre * metre);

            var oneFm = (1 * furlong).ConvertTo(metre);
            var oneCm = (1 * chain).ConvertTo(metre);
            var mm1 = oneCm * oneFm;
            
            var m = new Unit("metre", "m", BaseDimensions.Length);
            var fur = new Unit("furlong", "fur", m, 201.168m);
            var ch = new Unit("chain", "ch", m, 20.1168m);

            var fc = fur * ch;
            var mm = (1 * fc).ConvertTo(m * m);

            Assert.Equal(4046.8564224m, mm.Value);
        }
    }
}
