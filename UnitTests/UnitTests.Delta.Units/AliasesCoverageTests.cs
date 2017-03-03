using System.Diagnostics.CodeAnalysis;
using Delta.Units.Systems;
using Xunit;
using static Delta.Units.Systems.Aliases;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class AliaseslessCoverageTests
    {
        // Length

        [Fact]
        public void m_is_metre()
        {
            var u1 = 1.0 * m;
            var u2 = 1.0 * SI.metre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void km_is_kilometre()
        {
            var u1 = 1.0 * km;
            var u2 = 1.0 * SI.kilometre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void cm_is_centimetre()
        {
            var u1 = 1.0 * cm;
            var u2 = 1.0 * SI.centimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mm_is_millimetre()
        {
            var u1 = 1.0 * mm;
            var u2 = 1.0 * SI.millimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void micron_is_micrometre()
        {
            var u1 = 1.0 * micron;
            var u2 = 1.0 * SI.micrometre;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Time

        [Fact]
        public void ms_is_millisecond()
        {
            var u1 = 1.0 * ms;
            var u2 = 1.0 * SI.millisecond;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void s_is_second()
        {
            var u1 = 1.0 * s;
            var u2 = 1.0 * SI.second;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mn_is_minute()
        {
            var u1 = 1.0 * mn;
            var u2 = 1.0 * SI.minute;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void h_is_hour()
        {
            var u1 = 1.0 * h;
            var u2 = 1.0 * SI.hour;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Mass

        [Fact]
        public void g_is_gram()
        {
            var u1 = 1.0 * g;
            var u2 = 1.0 * SI.gram;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void kg_is_kilogram()
        {
            var u1 = 1.0 * kg;
            var u2 = 1.0 * SI.kilogram;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mg_is_milligram()
        {
            var u1 = 1.0 * mg;
            var u2 = 1.0 * SI.milligram;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void t_is_tonne()
        {
            var u1 = 1.0 * t;
            var u2 = 1.0 * SI.tonne;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Electric Current

        [Fact]
        public void A_is_Ampere()
        {
            var u1 = 1.0 * A;
            var u2 = 1.0 * SI.Ampere;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mA_is_Ampere()
        {
            var u1 = 1.0 * mA;
            var u2 = 1.0 * SI.milliAmpere;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Temperature

        [Fact]
        public void K_is_Kelvin()
        {
            var u1 = 1.0 * K;
            var u2 = 1.0 * SI.Kelvin;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void C_is_Celsius()
        {
            var u1 = 1.0 * C;
            var u2 = 1.0 * SI.Celsius;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void F_is_Fahrenheit()
        {
            var u1 = 1.0 * F;
            var u2 = 1.0 * SI.Fahrenheit;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Surfaces

        [Fact]
        public void sqm_is_square_metre()
        {
            var u1 = 1.0 * sqm;
            var u2 = 1.0 * SI.Area.square_metre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void sqcm_is_square_centimetre()
        {
            var u1 = 1.0 * sqcm;
            var u2 = 1.0 * SI.Area.square_centimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void sqkm_is_square_killometre()
        {
            var u1 = 1.0 * sqkm;
            var u2 = 1.0 * SI.Area.square_kilometre;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Volumes

        [Fact]
        public void ccm_is_cubic_metre()
        {
            var u1 = 1.0 * ccm;
            var u2 = 1.0 * SI.Volume.cubic_metre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void cccm_is_cubic_centimetre()
        {
            var u1 = 1.0 * cccm;
            var u2 = 1.0 * SI.Volume.cubic_centimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void ccmm_is_cubic_millimetre()
        {
            var u1 = 1.0 * ccmm;
            var u2 = 1.0 * SI.Volume.cubic_millimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Velocity

        [Fact]
        public void mps_is_metre_per_second()
        {
            var u1 = 1.0 * mps;
            var u2 = 1.0 * SI.Velocity.metre_per_second;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void kmph_is_kilometre_per_hour()
        {
            var u1 = 1.0 * kmph;
            var u2 = 1.0 * SI.Velocity.kilometre_per_hour;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Angles

        [Fact]
        public void rad_is_radian()
        {
            var u1 = 1.0 * rad;
            var u2 = 1.0 * Dimensionless.radian;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void deg_is_degree()
        {
            var u1 = 1.0 * deg;
            var u2 = 1.0 * Dimensionless.degree;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void tr_is_turn()
        {
            var u1 = 1.0 * tr;
            var u2 = 1.0 * Dimensionless.turn;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Proportions

        [Fact]
        public void ppm_is_parts_per_million()
        {
            var u1 = 1.0 * ppm;
            var u2 = 1.0 * Dimensionless.parts_per_million;
            Assert.Equal(u1.Value, u2.Value);
        }
    }
}
