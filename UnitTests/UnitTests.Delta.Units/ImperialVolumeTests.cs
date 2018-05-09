using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.SI;
using static Delta.Units.Systems.Imperial;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class ImperialVolumeTests
    {
        [Fact]
        public void One_ounce_is_0_0284130625_litres()
        {
            var oneOunce = 1 * ounce;
            var l = oneOunce.ConvertTo(Volume.litre);

            Assert.Equal(0.0284130625m, l.Value);
        }

        [Fact]
        public void One_ounce_is_1_73387145494763_cubic_inches()
        {
            var oneOunce = 1 * ounce;
            var cin = oneOunce.ConvertTo(cubic_inch);

            Assert.Equal(1.73387145494763m, cin.Value, 14);
        }
    }
}
