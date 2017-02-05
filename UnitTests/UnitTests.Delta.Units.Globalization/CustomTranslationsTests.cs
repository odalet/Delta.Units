using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;

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
                if (isFrench && u.Name == "kilogram") return "kilogramme";

                return u.Name;
            }

            public string TranslateSymbol(Unit unit, CultureInfo culture)
            {
                var u = unit ?? Unit.None;
                return u.Symbol;
            }
        }

        private class FailingProvider : IUnitTranslationProvider
        {
            public string TranslateName(Unit unit, CultureInfo culture)
            {
                throw new InvalidOperationException("TEST");
            }

            public string TranslateSymbol(Unit unit, CultureInfo culture)
            {
                throw new InvalidOperationException("TEST");
            }
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
