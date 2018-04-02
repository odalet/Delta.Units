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
    }
}
