using System.Diagnostics.CodeAnalysis;
using Delta.Units.Systems;
using Xunit;
using static Delta.Units.Systems.Aliases;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "OK in Unit Tests")]
    public class AliaseslessCoverageTests
    {
        // Length

        [Fact]        
        public void m_is_metre()
        {
            var u1 = 1m * m;
            var u2 = 1m * SI.metre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void km_is_kilometre()
        {
            var u1 = 1m * km;
            var u2 = 1m * SI.kilometre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void cm_is_centimetre()
        {
            var u1 = 1m * cm;
            var u2 = 1m * SI.centimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mm_is_millimetre()
        {
            var u1 = 1m * mm;
            var u2 = 1m * SI.millimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void micron_is_micrometre()
        {
            var u1 = 1m * micron;
            var u2 = 1m * SI.micrometre;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Time

        [Fact]
        public void ms_is_millisecond()
        {
            var u1 = 1m * ms;
            var u2 = 1m * SI.millisecond;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void s_is_second()
        {
            var u1 = 1m * s;
            var u2 = 1m * SI.second;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mn_is_minute()
        {
            var u1 = 1m * mn;
            var u2 = 1m * SI.minute;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void h_is_hour()
        {
            var u1 = 1m * h;
            var u2 = 1m * SI.hour;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Mass

        [Fact]
        public void g_is_gram()
        {
            var u1 = 1m * g;
            var u2 = 1m * SI.gram;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void kg_is_kilogram()
        {
            var u1 = 1m * kg;
            var u2 = 1m * SI.kilogram;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mg_is_milligram()
        {
            var u1 = 1m * mg;
            var u2 = 1m * SI.milligram;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void t_is_tonne()
        {
            var u1 = 1m * t;
            var u2 = 1m * SI.tonne;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Electric Current

        [Fact]
        public void A_is_Ampere()
        {
            var u1 = 1m * A;
            var u2 = 1m * SI.Ampere;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void mA_is_Ampere()
        {
            var u1 = 1m * mA;
            var u2 = 1m * SI.milliAmpere;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Temperature

        [Fact]
        public void K_is_Kelvin()
        {
            var u1 = 1m * K;
            var u2 = 1m * SI.Kelvin;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void C_is_Celsius()
        {
            var u1 = 1m * C;
            var u2 = 1m * SI.Celsius;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void F_is_Fahrenheit()
        {
            var u1 = 1m * F;
            var u2 = 1m * SI.Fahrenheit;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Surfaces

        [Fact]
        public void sqm_is_square_metre()
        {
            var u1 = 1m * sqm;
            var u2 = 1m * SI.Area.square_metre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void sqcm_is_square_centimetre()
        {
            var u1 = 1m * sqcm;
            var u2 = 1m * SI.Area.square_centimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void sqkm_is_square_killometre()
        {
            var u1 = 1m * sqkm;
            var u2 = 1m * SI.Area.square_kilometre;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Volumes

        [Fact]
        public void ccm_is_cubic_metre()
        {
            var u1 = 1m * ccm;
            var u2 = 1m * SI.Volume.cubic_metre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void cccm_is_cubic_centimetre()
        {
            var u1 = 1m * cccm;
            var u2 = 1m * SI.Volume.cubic_centimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void ccmm_is_cubic_millimetre()
        {
            var u1 = 1m * ccmm;
            var u2 = 1m * SI.Volume.cubic_millimetre;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Velocity

        [Fact]
        public void mps_is_metre_per_second()
        {
            var u1 = 1m * mps;
            var u2 = 1m * SI.Velocity.metre_per_second;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void kmph_is_kilometre_per_hour()
        {
            var u1 = 1m * kmph;
            var u2 = 1m * SI.Velocity.kilometre_per_hour;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Angles

        [Fact]
        public void rad_is_radian()
        {
            var u1 = 1m * rad;
            var u2 = 1m * Dimensionless.radian;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void deg_is_degree()
        {
            var u1 = 1m * deg;
            var u2 = 1m * Dimensionless.degree;
            Assert.Equal(u1.Value, u2.Value);
        }

        [Fact]
        public void tr_is_turn()
        {
            var u1 = 1m * tr;
            var u2 = 1m * Dimensionless.turn;
            Assert.Equal(u1.Value, u2.Value);
        }

        // Proportions

        [Fact]
        public void ppm_is_parts_per_million()
        {
            var u1 = 1m * ppm;
            var u2 = 1m * Dimensionless.parts_per_million;
            Assert.Equal(u1.Value, u2.Value);
        }
    }
}
