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
            if (innerProvider == null) return unit.Name;
            try
            {
                return innerProvider.TranslateName(unit, culture);
            }
            catch
            {
                return unit.Name;
            }
        }

        string IUnitTranslationProvider.TranslateSymbol(Unit unit, CultureInfo culture)
        {
            if (innerProvider == null) return unit.Symbol;
            try
            {
                return innerProvider.TranslateSymbol(unit, culture);
            }
            catch
            {
                return unit.Symbol;
            }
        }
    }
}
