using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.Imperial;
using static Delta.Units.Systems.SI;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class ImperialLengthTests
    {
        [Fact]
        public void One_thou_is_25_4_micrometre()
        {
            var oneThou = 1 * thou;
            var micrometres = oneThou.ConvertTo(micrometre);

            Assert.Equal(25.4m, micrometres.Value);
        }

        [Fact]
        public void One_inch_is_2_54_cm()
        {
            var oneInch = 1 * inch;
            var centimetres = oneInch.ConvertTo(centimetre);

            Assert.Equal(2.54m, centimetres.Value);
        }

        [Fact]
        public void One_foot_is_304_8_mm()
        {
            var oneFoot = 1 * foot;
            var millimetres = oneFoot.ConvertTo(millimetre);

            Assert.Equal(304.8m, millimetres.Value);
        }

        [Fact]
        public void One_yard_is_0_9144_metres()
        {
            var oneYard = 1 * yard;
            var metres = oneYard.ConvertTo(metre);

            Assert.Equal(0.9144m, metres.Value);
        }

        [Fact]
        public void One_chain_is_20_1168_metres()
        {
            var oneChain = 1 * chain;
            var metres = oneChain.ConvertTo(metre);

            Assert.Equal(20.1168m, metres.Value);
        }

        [Fact]
        public void One_furlong_is_201_168_metres()
        {
            var oneFurlong = 1 * furlong;
            var metres = oneFurlong.ConvertTo(metre);

            Assert.Equal(201.168m, metres.Value);
        }

        [Fact]
        public void One_mile_is_1_609344_kilometres()
        {
            var oneMile = 1 * mile;
            var kilometres = oneMile.ConvertTo(kilometre);

            Assert.Equal(1.609344m, kilometres.Value);
        }

        [Fact]
        public void One_link_is_1_100th_of_a_chain()
        {
            var oneLink = 1 * link;
            var chains = oneLink.ConvertTo(chain);

            Assert.Equal(0.01m, chains.Value);
        }

        [Fact]
        public void One_rod_is_5_5_yards()
        {
            var oneRod = 1 * rod;
            var yards = oneRod.ConvertTo(yard);

            Assert.Equal(5.5m, yards.Value);
        }

        [Fact]
        public void One_fathom_is_1_852_metres()
        {
            var oneFathom = 1 * fathom;
            var metres = oneFathom.ConvertTo(metre);

            Assert.Equal(1.852m, metres.Value);
        }

        [Fact]
        public void One_cable_is_185_2_metres()
        {
            var oneCable = 1 * cable;
            var metres = oneCable.ConvertTo(metre);

            Assert.Equal(185.2m, metres.Value);
        }

        [Fact]
        public void One_nautical_mile_is_1_852_kilometres()
        {
            var oneNauticalMile = 1 * nautical_mile;
            var kilometres = oneNauticalMile.ConvertTo(kilometre);

            Assert.Equal(1.852m, kilometres.Value);
        }
    }
}
