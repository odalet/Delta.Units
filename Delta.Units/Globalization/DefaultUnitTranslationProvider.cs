using System;
using System.Globalization;

namespace Delta.Units.Globalization
{
    /// <summary>
    /// Default implementation of <see cref="IUnitTranslationProvider"/>
    /// </summary>
    /// <seealso cref="Delta.Units.Globalization.IUnitTranslationProvider" />
    public class DefaultUnitTranslationProvider : IUnitTranslationProvider
    {
        private static readonly DefaultUnitTranslationProvider wrapper = new DefaultUnitTranslationProvider();
        private IUnitTranslationProvider innerProvider;

        private DefaultUnitTranslationProvider() { }

        /// <summary>
        /// Gets or sets the current translation provider.
        /// </summary>
        public static IUnitTranslationProvider Current
        {
            get { return wrapper; }
            set { wrapper.innerProvider = value == wrapper ? null : value; }
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
            if (innerProvider == null || innerProvider == this) return unit.Symbol;
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
