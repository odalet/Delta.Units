using System.Globalization;

namespace Delta.Units.Globalization
{
    /// <summary>
    /// This interface defines the two methods to implement in order to internationalize a unit.
    /// </summary>
    public interface IUnitTranslationProvider
    {
        /// <summary>
        /// Translates the symbol of the specified unit according to the suplpied culture.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Translated unit symbol</returns>
        string TranslateSymbol(Unit unit, CultureInfo culture);

        /// <summary>
        /// Translates the name of the specified unit according to the suplpied culture.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Translated unit name</returns>
        string TranslateName(Unit unit, CultureInfo culture);
    }
}
