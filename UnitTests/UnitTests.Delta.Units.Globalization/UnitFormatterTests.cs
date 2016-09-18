using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;

namespace Delta.Units.Globalization
{
    [ExcludeFromCodeCoverage]
    public class UnitFormatterTests
    {
        [Fact]
        public void FormatSimpleUnitNoArgsTests()
        {
            var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);

            var expected = "kg";
            var actual1 = UnitFormatter.Format(kg, null, null);
            var actual2 = UnitFormatter.Format(kg, string.Empty, null);
            var actual3 = kg.ToString();
            var actual4 = kg.ToString(string.Empty);
            var actual5 = kg.ToString((string)null);
            var actual6 = kg.ToString((IFormatProvider)null);
            var actual7 = kg.ToString(null, null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
            Assert.Equal(expected, actual7);
        }

        [Fact]
        public void FormatNoneUnitNoArgsTests()
        {
            Unit none = Unit.None;

            var expected = string.Empty;
            var actual1 = UnitFormatter.Format(none, null, null);
            var actual2 = UnitFormatter.Format(none, string.Empty, null);
            var actual3 = none.ToString();
            var actual4 = none.ToString(string.Empty);
            var actual5 = none.ToString((string)null);
            var actual6 = none.ToString((IFormatProvider)null);
            var actual7 = none.ToString(null, null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
            Assert.Equal(expected, actual7);
        }


        [Fact]
        public void FormatNoneUnitAliasNoArgsTests()
        {
            var none = new Unit("None Unit", "n", Unit.None);

            var expected = "n";
            var actual1 = UnitFormatter.Format(none, null, null);
            var actual2 = UnitFormatter.Format(none, string.Empty, null);
            var actual3 = none.ToString();
            var actual4 = none.ToString(string.Empty);
            var actual5 = none.ToString((string)null);
            var actual6 = none.ToString((IFormatProvider)null);
            var actual7 = none.ToString(null, null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
            Assert.Equal(expected, actual7);
        }

        [Fact]
        public void FormatNullUnitNoArgsTests()
        {
            Unit none = null;

            var expected = string.Empty;
            var actual1 = UnitFormatter.Format(none, null, null);
            var actual2 = UnitFormatter.Format(none, string.Empty, null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
        }

        [Fact]
        public void FormatSimpleUnitToNameTests()
        {
            var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);

            var expected = "kilogram";
            var actual1 = UnitFormatter.Format(kg, "N", null);
            var actual2 = UnitFormatter.Format(kg, "n", null);
            var actual3 = kg.ToString("N");
            var actual4 = kg.ToString("n");
            var actual5 = kg.ToString("N", null);
            var actual6 = kg.ToString("n", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
        }

        [Fact]
        public void FormatNoneUnitToNameTests()
        {
            var none = Unit.None;

            var expected = string.Empty;
            var actual1 = UnitFormatter.Format(none, "N", null);
            var actual2 = UnitFormatter.Format(none, "n", null);
            var actual3 = none.ToString("N");
            var actual4 = none.ToString("n");
            var actual5 = none.ToString("N", null);
            var actual6 = none.ToString("n", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
        }

        [Fact]
        public void FormatNoneUnitAliasToNameTests()
        {
            var none = new Unit("None Unit", "n", Unit.None);

            var expected = "None Unit";
            var actual1 = UnitFormatter.Format(none, "N", null);
            var actual2 = UnitFormatter.Format(none, "n", null);
            var actual3 = none.ToString("N");
            var actual4 = none.ToString("n");
            var actual5 = none.ToString("N", null);
            var actual6 = none.ToString("n", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
        }

        [Fact]
        public void FormatNullUnitToNameTests()
        {
            Unit none = null;

            var expected = string.Empty;
            var actual1 = UnitFormatter.Format(none, "N", null);
            var actual2 = UnitFormatter.Format(none, "n", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
        }

        [Fact]
        public void FormatSimpleUnitToSymbolTests()
        {
            var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);

            var expected = "kg";
            var actual1 = UnitFormatter.Format(kg, "S", null);
            var actual2 = UnitFormatter.Format(kg, "s", null);
            var actual3 = kg.ToString("S");
            var actual4 = kg.ToString("s");
            var actual5 = kg.ToString("S", null);
            var actual6 = kg.ToString("s", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
        }

        [Fact]
        public void FormatNoneUnitToSymbolTests()
        {
            var none = Unit.None;

            var expected = string.Empty;
            var actual1 = UnitFormatter.Format(none, "S", null);
            var actual2 = UnitFormatter.Format(none, "s", null);
            var actual3 = none.ToString("S");
            var actual4 = none.ToString("s");
            var actual5 = none.ToString("S", null);
            var actual6 = none.ToString("s", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
        }

        [Fact]
        public void FormatNoneUnitAliasToSymbolTests()
        {
            var none = new Unit("None Unit", "n", Unit.None);

            var expected = "n";
            var actual1 = UnitFormatter.Format(none, "S", null);
            var actual2 = UnitFormatter.Format(none, "s", null);
            var actual3 = none.ToString("S");
            var actual4 = none.ToString("s");
            var actual5 = none.ToString("S", null);
            var actual6 = none.ToString("s", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
            Assert.Equal(expected, actual4);
            Assert.Equal(expected, actual5);
            Assert.Equal(expected, actual6);
        }

        [Fact]
        public void FormatNullUnitToSymbolTests()
        {
            Unit none = null;

            var expected = string.Empty;
            var actual1 = UnitFormatter.Format(none, "S", null);
            var actual2 = UnitFormatter.Format(none, "s", null);

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
        }

        [Fact]
        public void FormatUnitInvalidFormatTests()
        {
            var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);
            Assert.Throws<FormatException>(() => kg.ToString("z"));
            Assert.Throws<FormatException>(() => kg.ToString("zzzz"));
        }

        [Fact]
        public void FormatUnitWithNullPartsTests()
        {
            var nullUnit = new Unit(null, null, BaseDimensions.Mass);
            Assert.Equal(string.Empty, nullUnit.ToString());
            Assert.Equal(string.Empty, nullUnit.ToString("S"));
            Assert.Equal(string.Empty, nullUnit.ToString("N"));
        }
    }
}
