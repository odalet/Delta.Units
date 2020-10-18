using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;
using Delta.Units.Systems;

namespace Delta.Units.Globalization
{
    [ExcludeFromCodeCoverage]
    public class CustomTranslationsTests
    {
        private class SimpleProvider : IUnitTranslationProvider
        {
            public string TranslateName(Unit unit, CultureInfo culture)
            {
                var u = unit ?? Unit.None;
                var isFrench = culture != null && culture.Name.StartsWith("fr");
                return isFrench && u.Name == "kilogram" ? "kilogramme" : u.Name;
            }

            public string TranslateSymbol(Unit unit, CultureInfo culture)
            {
                var u = unit ?? Unit.None;
                return u.Symbol;
            }
        }

        private class FailingProvider : IUnitTranslationProvider
        {
            public string TranslateName(Unit unit, CultureInfo culture) => throw new InvalidOperationException("TEST");

            public string TranslateSymbol(Unit unit, CultureInfo culture) => throw new InvalidOperationException("TEST");
        }

        [Fact]
        public void LocalTranslationsAreAppliedWhenInvokingToString()
        {
            var meter = new Unit("american meter", "am", SI.metre)
            {
                TranslateNameFunction = c => "meter",
                TranslateSymbolFunction = c => "m"
            };

            var oneMeter = 1m * meter;
            Assert.Equal("1 m", oneMeter.ToString());
            Assert.Equal("1 meter", oneMeter.ToString("N"));
        }

        [Fact]
        [SuppressMessage("Minor Code Smell", "S3626:Jump statements should not be redundant", Justification = "Required for the test")]
        public void DefaultTranslationProviderIsUsedWhenLocalTranslationThrows()
        {
            var meter = new Unit("american meter", "am", SI.metre)
            {
                TranslateNameFunction = _ => throw new InvalidOperationException("TEST"),
                TranslateSymbolFunction = _ => throw new InvalidOperationException("TEST")
            };

            var oneMeter = 1m * meter;
            Assert.Equal("1 am", oneMeter.ToString());
            Assert.Equal("1 american meter", oneMeter.ToString("N"));
        }

        [Fact]
        public void NullTranslationProviderTest()
        {
            var previous = DefaultUnitTranslationProvider.Current;
            try
            {
                DefaultUnitTranslationProvider.Current = null;
                var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);
                Assert.Equal("kilogram", kg.ToString("N"));
                Assert.Equal("kg", kg.ToString("S"));
            }
            finally
            {
                DefaultUnitTranslationProvider.Current = previous;
            }
        }

        [Fact]
        public void CustomTranslationProviderTest()
        {
            var previousCulture = CultureInfo.DefaultThreadCurrentCulture;
            var previous = DefaultUnitTranslationProvider.Current;
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
                DefaultUnitTranslationProvider.Current = new SimpleProvider();
                var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);
                Assert.Equal("kilogramme", kg.ToString("N"));
                Assert.Equal("kg", kg.ToString("S"));
            }
            finally
            {
                CultureInfo.DefaultThreadCurrentCulture = previousCulture;
                DefaultUnitTranslationProvider.Current = previous;
            }
        }

        [Fact]
        public void FailingTranslationProviderTest()
        {
            var previous = DefaultUnitTranslationProvider.Current;
            try
            {
                DefaultUnitTranslationProvider.Current = new FailingProvider();
                var kg = new Unit("kilogram", "kg", BaseDimensions.Mass);
                // Even though the provider fails, translation should not throw, and instead revert to the fallback bahavior
                Assert.Equal("kilogram", kg.ToString("N"));
                Assert.Equal("kg", kg.ToString("S"));
            }
            finally
            {
                DefaultUnitTranslationProvider.Current = previous;
            }
        }
    }
}
