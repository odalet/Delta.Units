using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.Aliases;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class SIPrefixTests
    {
        [Fact]
        public void KilometreIsAThousandMetre() => Assert.Equal((1 * km).ConvertTo(m).Value, (1000 * m).Value);

        [Fact]
        public void MillimetreIsAThousandthMetre() => Assert.Equal((1 * mm).ConvertTo(m).Value, (1.0 / 1000 * m).Value);
    }
}
