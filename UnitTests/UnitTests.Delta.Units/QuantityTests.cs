using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;
using static Delta.Units.Systems.SI;
using static Delta.Units.Systems.SI.Area;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class QuantityTests
    {
        // Formatting

        [Fact]
        public void QuantityToStringIsCorrectlyFormatted()
        {
            var val = 42.24m;
            var valAsString = val.ToString(); // Uses the current culture (i.e. 42,24 for French cultures).

            var q = val * metre;
            Assert.Equal(valAsString + " m", q.ToString());
            Assert.Equal(valAsString + " metre", q.ToString("N"));
        }

        [Fact]
        public void QuantityToStringWithSpecificCultureIsCorrectlyFormatted()
        {
            var q = 42.24m * metre;
            // Uses the invariant current culture --> 42.24
            Assert.Equal("42.24 m", q.ToString(CultureInfo.InvariantCulture));
            Assert.Equal("42.24 metre", q.ToString("N", CultureInfo.InvariantCulture));
        }

        // Operations

        [Fact]
        public void QuantityPlusQuantityIsQuantity()
        {
            var q1 = 1.1 * metre;
            var q2 = 2.2 * metre;
            var sum = q1 + q2;

            Assert.Equal(3.3m, sum.Value);
        }

        [Fact]
        public void QuantitySumResultUsesTheLeftUnit()
        {
            var q1 = 1 * metre;
            var q2 = 2 * kilometre;
            var sum = q1 + q2;

            Assert.Equal(2001, sum.Value); // the result is in metres
        }

        [Fact]
        public void QuantityMinusQuantityIsQuantity()
        {
            var q1 = 2.2 * metre;
            var q2 = 1.1 * metre;
            var sum = q1 - q2;

            Assert.Equal(1.1m, sum.Value);
        }

        [Fact]
        public void QuantityDifferenceResultUsesTheLeftUnit()
        {
            var q1 = 1 * metre;
            var q2 = 2 * kilometre;
            var sum = q1 - q2;

            Assert.Equal(-1999, sum.Value); // the result is in metres
        }

        [Fact]
        public void Quantity_product_is_product_of_values_and_product_of_units()
        {
            var q1 = 2 * metre;
            var q2 = 3 * centimetre;
            var q = q1 * q2;
            var sqm = q.ConvertTo(square_metre);

            Assert.Equal(0.06m, sqm.Value);
        }

        [Fact]
        public void Quantity_quotient_is_quotient_of_values_and_quotient_of_units()
        {
            var q = 4 * square_metre;
            var div = 2 * metre;
            var result = q / div; // TODO: unit is represented as m²/m; should be simplified to m...

            Assert.Equal(2m, result.Value);
        }
    }
}
