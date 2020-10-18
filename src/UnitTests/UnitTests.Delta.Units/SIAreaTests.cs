using Xunit;
using static Delta.Units.Systems.SI.Area;

namespace Delta.Units
{
    public class SIAreaTests
    {
        [Fact]
        public void One_are_is_100_square_metres()
        {
            var _m = new Unit("m", "m", BaseDimensions.Length);
            var _a = new Unit("a", "a", _m * _m, 100m);

            var oneAre = 1 * _a;
            var sqm = oneAre.ConvertTo(_m * _m);
            Assert.Equal(100m, sqm.Value);
        }

        [Fact]
        public void One_hectare_is_10000_square_metres()
        {
            var oneHectare = 1 * hectare;
            var sqm = oneHectare.ConvertTo(square_metre);
            Assert.Equal(10000m, sqm.Value);
        }
    }
}
