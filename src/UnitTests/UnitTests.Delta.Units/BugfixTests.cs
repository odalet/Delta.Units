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
        
        [Fact]
        public void Two_different_definitions_of_the_same_compound_should_work_the_same()
        {
            var m = new Unit("m", "m", BaseDimensions.Length);

            // 10 m * 10 m = 100 m²
            var are = new Unit("are", "a", m * m, 100m);
            var oneAre = 1 * are;
            var sqm1 = oneAre.ConvertTo(m * m);

            // 1 dam = 10 m
            var dam = new Unit("dam", "dam", m, 10m);
            // 1 dam² = 1 * dam * dam = 100 m²
            var dam2 = new Unit("dam2", "dam2", dam * dam, 1m);
            var oneDam2 = 1 * dam2;
            var sqm2 = oneDam2.ConvertTo(m * m);

            Assert.Equal(sqm1.Value, sqm2.Value); // both should be 4
        }
    }
}
