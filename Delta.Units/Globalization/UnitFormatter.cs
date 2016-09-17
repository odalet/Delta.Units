using System;
using System.Globalization;

namespace Delta.Units.Globalization
{
    internal static class UnitFormatter
    {
        private const string symbolFormat = "S";
        private const string nameFormat = "N";
        private const string defaultFormat = symbolFormat;

        public static readonly string[] FormatSpecifiers = { symbolFormat, nameFormat };

        public static string Format(Unit unit, string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = defaultFormat;
            switch (format.ToUpperInvariant())
            {
                case symbolFormat: return TranslateSymbol(unit, GetCulture(formatProvider));
                case nameFormat: return TranslateName(unit, GetCulture(formatProvider));
            }

            throw new FormatException($"Invalid format specifier: '{format}'");
        }

        private static string TranslateSymbol(Unit unit, CultureInfo culture) =>
            (unit ?? Unit.None).TranslationProvider.TranslateSymbol(unit, culture);

        private static string TranslateName(Unit unit, CultureInfo culture) =>
            (unit ?? Unit.None).TranslationProvider.TranslateName(unit, culture);

        private static CultureInfo GetCulture(IFormatProvider formatProvider)
        {
            var culture = formatProvider as CultureInfo;
            return culture ?? CultureInfo.CurrentCulture;
        }
    }
}
