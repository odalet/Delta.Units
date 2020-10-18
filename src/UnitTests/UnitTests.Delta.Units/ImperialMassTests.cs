using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.Imperial;
using static Delta.Units.Systems.SI;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class ImperialMassTests
    {
        [Fact]
        public void One_grain_is_64_79891_milligrams()
        {
            var oneGrain = 1 * grain;
            var mg = oneGrain.ConvertTo(milligram);

            Assert.Equal(64.79891m, mg.Value);
        }

        [Fact]
        public void One_drachm_is_1_7718451953125_grams()
        {
            var oneDrachm = 1 * drachm;
            var g = oneDrachm.ConvertTo(gram);

            Assert.Equal(1.7718451953125m, g.Value);
        }

        [Fact]
        public void One_ounce_is_28_349523125_grams()
        {
            var oneOunce = 1 * ounce;
            var g = oneOunce.ConvertTo(gram);

            Assert.Equal(28.349523125m, g.Value);
        }

        [Fact]
        public void One_pound_is_453_59237_grams()
        {
            var onePound = 1 * pound;
            var g = onePound.ConvertTo(gram);

            Assert.Equal(453.59237m, g.Value);
        }

        [Fact]
        public void One_stone_is_6_35029318_kilograms()
        {
            var oneStone = 1 * stone;
            var kg = oneStone.ConvertTo(kilogram);

            Assert.Equal(6.35029318m, kg.Value);
        }

        [Fact]
        public void One_quarter_is_12_70058636_kilograms()
        {
            var oneQuarter = 1 * quarter;
            var kg = oneQuarter.ConvertTo(kilogram);

            Assert.Equal(12.70058636m, kg.Value);
        }

        [Fact]
        public void One_hundredweight_is_50_80234544_kilograms()
        {
            var oneHundredweight = 1 * hundredweight;
            var kg = oneHundredweight.ConvertTo(kilogram);

            Assert.Equal(50.80234544m, kg.Value);
        }

        [Fact]
        public void One_ton_is_1016_0469088_kilograms()
        {
            var oneTon = 1 * ton;
            var kg = oneTon.ConvertTo(kilogram);

            Assert.Equal(1016.0469088m, kg.Value);
        }
    }
}
