using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;
using static Delta.Units.Systems.SI;

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
            Assert.Equal(q.ToString(), valAsString + " m");
            Assert.Equal(q.ToString("N"), valAsString + " metre");
        }

        [Fact]
        public void QuantityToStringWithSpecificCultureIsCorrectlyFormatted()
        {
            var q = 42.24m * metre;
            // Uses the invariant current culture --> 42.24
            Assert.Equal(q.ToString(CultureInfo.InvariantCulture), "42.24 m");
            Assert.Equal(q.ToString("N", CultureInfo.InvariantCulture), "42.24 metre");
        }

        // Operations

        [Fact]
        public void QuantityPlusQuantityIsQuantity()
        {
            var q1 = 1.1 * metre;
            var q2 = 2.2 * metre;
            var sum = q1 + q2;

            Assert.Equal(sum.Value, 3.3m);
        }

        public void QuantitySumResultUsesTheLeftUnit()
        {
            var q1 = 1 * metre;
            var q2 = 2 * kilometre;
            var sum = q1 + q2;

            Assert.Equal(sum.Value, 2001); // the result is in metres
        }

        [Fact]
        public void QuantityMinusQuantityIsQuantity()
        {
            var q1 = 2.2 * metre;
            var q2 = 1.1 * metre;
            var sum = q1 - q2;

            Assert.Equal(sum.Value, 1.1m);
        }

        public void QuantityDifferenceResultUsesTheLeftUnit()
        {
            var q1 = 1 * metre;
            var q2 = 2 * kilometre;
            var sum = q1 - q2;

            Assert.Equal(sum.Value, -1999); // the result is in metres
        }
    }
}
