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
            var val = 42.24;
            var valAsString = val.ToString(); // Uses the current culture (i.e. 42,24 for French cultures).

            var q = val * metre;
            Assert.Equal(q.ToString(), valAsString + " m");
            Assert.Equal(q.ToString("N"), valAsString + " metre");
        }

        [Fact]
        public void QuantityToStringWithSpecificCultureIsCorrectlyFormatted()
        {
            var q = 42.24 * metre;
            // Uses the invariant current culture --> 42.24
            Assert.Equal(q.ToString(CultureInfo.InvariantCulture), "42.24 m");
            Assert.Equal(q.ToString("N", CultureInfo.InvariantCulture), "42.24 metre");
        }

        // Operations

        ////[Fact]
        ////public void QuantityPlusQualtityIsQuantity()
        ////{
        ////    var q1 = 1.1 * metre;
        ////    var q2 = 2.2 * metre;
        ////    var sum = q1 + q2;

        ////    Assert.Equal(sum.Value, 3.3);
        ////}
    }
}
