using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Delta.Units.Systems;
using Xunit;

namespace Delta.Units.Globalization
{
    [ExcludeFromCodeCoverage]
    public class QuantityFormatterTests
    {
        [Fact]
        public void Rtl_is_honored_when_ltr_ui_culture()
        {
            var q = 42 * SI.metre;
            var savedCulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
                Assert.Equal("42 m", QuantityFormatter.Format(q, "", null));
            }
            finally 
            {
                CultureInfo.CurrentUICulture = savedCulture;
            }
        }

        [Fact]
        public void Rtl_is_honored_when_rtl_ui_culture()
        {
            var q = 42 * SI.metre;
            var savedCulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AR");
                Assert.Equal("m 42", QuantityFormatter.Format(q, "", null));
            }
            finally
            {
                CultureInfo.CurrentUICulture = savedCulture;
            }
        }

        [Fact]
        public void Rtl_is_honored_when_explicit_ltr()
        {
            var q = 42 * SI.metre;
            Assert.Equal("42 m", QuantityFormatter.Format(q, "", null, false));
        }

        [Fact]
        public void Rtl_is_honored_when_explicit_rtl()
        {
            var q = 42 * SI.metre;
            Assert.Equal("m 42", QuantityFormatter.Format(q, "", null, true));
        }

        [Fact]
        public void No_space_between_value_and_unit_if_degree_ltr()
        {
            var q = 42 * Dimensionless.degree;
            Assert.Equal("42°", QuantityFormatter.Format(q, "", null, false));
        }

        [Fact]
        public void Space_between_unit_and_value_if_degree_rtl()
        {
            var q = 42 * Dimensionless.degree;
            Assert.Equal("° 42", QuantityFormatter.Format(q, "", null, true));
        }

        [Fact]
        public void QuantityFormatter_throws_when_passed_null_quantity() => 
            Assert.Throws<ArgumentNullException>(() => QuantityFormatter.Format(null, "", null, true));
    }
}
