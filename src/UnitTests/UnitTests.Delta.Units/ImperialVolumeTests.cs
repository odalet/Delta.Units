using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.Imperial;
using static Delta.Units.Systems.SI;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class ImperialVolumeTests
    {
        [Fact]
        public void One_ounce_is_0_0284130625_litres()
        {
            var oneOunce = 1 * fluid_ounce;
            var l = oneOunce.ConvertTo(Volume.litre);

            Assert.Equal(0.0284130625m, l.Value);
        }

        [Fact]
        public void One_ounce_is_1_73387145494763_cubic_inches()
        {
            var oneOunce = 1 * fluid_ounce;
            var cin = oneOunce.ConvertTo(cubic_inch);

            Assert.Equal(1.73387145494763m, cin.Value, 14);
        }

        [Fact]
        public void One_gill_is_142_0653125_millilitres()
        {
            var oneGill = 1 * gill;
            var ml = oneGill.ConvertTo(Volume.millilitre);

            Assert.Equal(142.0653125m, ml.Value);
        }

        [Fact]
        public void One_pint_is_568_26125_millilitres()
        {
            var onePint = 1 * pint;
            var ml = onePint.ConvertTo(Volume.millilitre);

            Assert.Equal(568.26125m, ml.Value);
        }

        [Fact]
        public void One_quart_is_1_1365225_litres()
        {
            var oneQuart = 1 * quart;
            var l = oneQuart.ConvertTo(Volume.litre);

            Assert.Equal(1.1365225m, l.Value);
        }

        [Fact]
        public void One_gallon_is_4_54609_litres()
        {
            var oneGallon = 1 * gallon;
            var l = oneGallon.ConvertTo(Volume.litre);

            Assert.Equal(4.54609m, l.Value);
        }
    }
}
