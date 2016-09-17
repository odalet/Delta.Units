using System;
using System.Globalization;

namespace Delta.Units.Globalization
{
    public class DefaultUnitTranslationProvider : IUnitTranslationProvider
    {
        private static readonly DefaultUnitTranslationProvider wrapper = new DefaultUnitTranslationProvider();
        private IUnitTranslationProvider innerProvider;

        private DefaultUnitTranslationProvider() { }

        public static IUnitTranslationProvider Current
        {
            get { return wrapper; }
            set { wrapper.innerProvider = value; }
        }

        string IUnitTranslationProvider.TranslateName(Unit unit, CultureInfo culture) 
        {
            if (innerProvider == null) return GetName(unit);
            try
            {
                return innerProvider.TranslateName(unit, culture);
            }
            catch
            {
                return GetName(unit);
            }
        }

        string IUnitTranslationProvider.TranslateSymbol(Unit unit, CultureInfo culture)
        {
            if (innerProvider == null) return GetSymbol(unit);
            try
            {
                return innerProvider.TranslateSymbol(unit, culture);
            }
            catch
            {
                return GetSymbol(unit);
            }
        }

        private string GetName(Unit unit) => unit == null ? string.Empty : unit.Name ?? string.Empty;
        private string GetSymbol(Unit unit) => unit == null ? string.Empty : unit.Symbol ?? string.Empty;
    }
}
