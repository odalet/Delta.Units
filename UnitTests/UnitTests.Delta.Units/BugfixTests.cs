using Delta.Units;
using Xunit;

namespace Delta.Units
{
    public class BugfixTests
    {
        [Fact]
        public void One_are_is_100_square_metres()
        {
            var m = new Unit("m", "m", BaseDimensions.Length);
            var a = new Unit("a", "a", m * m, 100m);

            var oneAre = 1 * a;
            var sqm = oneAre.ConvertTo(m * m);
            Assert.Equal(100m, sqm.Value);
        }
    }
}
