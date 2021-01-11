using System;
using System.Globalization;
using System.Linq;

namespace Delta.Units.Globalization
{
    /// <summary>
    /// Helper clas that formats a <see cref="Quantity"/> to a string.
    /// </summary>
    public static class QuantityFormatter
    {
        /// <summary>
        /// Formats the specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="rightToLeft">if <c>true</c>, right to left formatting; otherwise, left to right. If null, uses CultureInfo.CurrentUICulture settings</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">quantity</exception>
        /// <exception cref="FormatException"></exception>
        public static string Format(Quantity quantity, string format, IFormatProvider formatProvider, bool? rightToLeft = null)
        {
            if (quantity == null) throw new ArgumentNullException(nameof(quantity));

            try
            {
                var numberFormat = string.Empty;
                var unitFormat = string.Empty;

                var subFormats = (format ?? string.Empty).Split(';');
                if (subFormats.Length == 1)
                {
                    var subFormat = subFormats[0];
                    if (UnitFormatter.FormatSpecifiers.Contains(subFormat, StringComparer.OrdinalIgnoreCase))
                        unitFormat = subFormat;
                    else numberFormat = subFormat;
                }
                else // Note: 0 component is impossible.
                {
                    // only the two first components are processed. Subsequent ones are ignored.                    
                    numberFormat = subFormats[0];
                    unitFormat = subFormats[1];                    
                }

                var numberAsString = quantity.Value.ToString(numberFormat, formatProvider);
                var unitAsString = UnitFormatter.Format(quantity.Unit, unitFormat, formatProvider);
                return Combine(numberAsString, unitAsString, rightToLeft ?? CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft);
            }
            catch (FormatException fex)
            {
                throw new FormatException($"Invalid format specifier: '{format}'", fex);
            }
        }
        
        private static string Combine(string number, string unit, bool rtl)
        {
            // According to Google Translate, the unit should be placed before the number 
            // and there is always a space between the unit an the number.
            // Only tested by translating to Arabic, Hebrew and Persian.
            if (rtl) 
                return $"{unit} {number}";
            else
            {
                var space = unit.StartsWith("°") ? string.Empty : " ";
                return $"{number}{space}{unit}";
            }
        }
    }
}
