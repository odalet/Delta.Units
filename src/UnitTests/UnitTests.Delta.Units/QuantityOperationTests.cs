using System.Diagnostics.CodeAnalysis;
using Delta.Units.Systems;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class QuantityOperationTests
    {
        [Fact]
        public void Quantity_plus_decimal() => Assert.Equal(52m, (42m * SI.metre + 10m).Value);

        [Fact]
        public void Decimal_plus_quantity() => Assert.Equal(52m, (10m + 42m * SI.metre).Value);

        [Fact]
        public void Quantity_minus_decimal() => Assert.Equal(32m, (42m * SI.metre - 10m).Value);

        [Fact]
        public void Decimal_minus_quantity() => Assert.Equal(-32m, (10m - 42m * SI.metre).Value);

        [Fact]
        public void Quantity_multiplied_by_decimal() => Assert.Equal(420m, (42m * SI.metre * 10m).Value);

        [Fact]
        public void Decimal_multiplied_by_quantity() => Assert.Equal(420m, (10m * (42m * SI.metre)).Value);

        [Fact]
        public void Quantity_divided_by_decimal() => Assert.Equal(4.2m, (42 * SI.metre / 10m).Value);

        [Fact]
        public void Quantity_plus_double() => Assert.Equal(52m, (42.0 * SI.metre + 10.0).Value);

        [Fact]
        public void Double_plus_quantity() => Assert.Equal(52m, (10.0 + 42.0 * SI.metre).Value);

        [Fact]
        public void Quantity_minus_double() => Assert.Equal(32m, (42.0 * SI.metre - 10.0).Value);

        [Fact]
        public void Double_minus_quantity() => Assert.Equal(-32m, (10.0 - 42.0 * SI.metre).Value);

        [Fact]
        public void Quantity_multiplied_by_double() => Assert.Equal(420m, (42.0 * SI.metre * 10.0).Value);

        [Fact]
        public void Double_multiplied_by_quantity() => Assert.Equal(420m, (10.0 * (42.0 * SI.metre)).Value);

        [Fact]
        public void Quantity_divided_by_double() => Assert.Equal(4.2m, (42.0 * SI.metre / 10.0).Value);

        [Fact]
        public void Quantity_plus_int() => Assert.Equal(52m, (42 * SI.metre + 10).Value);

        [Fact]
        public void Int_plus_quantity() => Assert.Equal(52m, (10 + 42 * SI.metre).Value);

        [Fact]
        public void Quantity_minus_int() => Assert.Equal(32m, (42 * SI.metre - 10).Value);

        [Fact]
        public void Int_minus_quantity() => Assert.Equal(-32m, (10 - 42 * SI.metre).Value);

        [Fact]
        public void Quantity_multiplied_by_int() => Assert.Equal(420m, (42 * SI.metre * 10).Value);

        [Fact]
        public void Int_multiplied_by_quantity() => Assert.Equal(420m, (10 * (42 * SI.metre)).Value);

        [Fact]
        public void Quantity_divided_by_int() => Assert.Equal(4.2m, (42 * SI.metre / 10).Value);

        [Fact]
        public void Quantity_plus() => Assert.Equal(42m, (+(42m * SI.metre)).Value);

        [Fact]
        public void Quantity_minus() => Assert.Equal(-42m, (-(42m * SI.metre)).Value);

        [Fact]
        public void Quantity_plus_quantity() => Assert.Equal(84m, (42 * SI.metre + 42 * SI.metre).Value);

        [Fact, SuppressMessage("Major Bug", "S1764:Identical expressions should not be used on both sides of a binary operator", Justification = "<Pending>")]
        public void Quantity_minus_quantity() => Assert.Equal(21m, (42 * SI.metre - 21 * SI.metre).Value);

        [Fact]
        public void Quantity_multiplied_by_quantity()
        {
            var result = 42m * SI.metre * (42m * SI.metre);
            Assert.Equal(42m * 42m, result.Value);
            Assert.Equal(SI.Area.square_metre.Dimension, result.Unit.Dimension);
        }

        [Fact]
        public void Quantity_divided_by_quantity()
        {
            var result = 42m * SI.metre / (21m * SI.metre);
            Assert.Equal(2m, result.Value);
            Assert.True(result.Unit.IsNone());
        }
    }
}
