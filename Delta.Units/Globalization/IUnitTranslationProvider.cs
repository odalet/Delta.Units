using System.Globalization;

namespace Delta.Units.Globalization
{
    public interface IUnitTranslationProvider
    {
        string TranslateSymbol(Unit unit, CultureInfo culture);
        string TranslateName(Unit unit, CultureInfo culture);
    }
}
