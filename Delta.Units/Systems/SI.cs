using System.Collections.Generic;
using static Delta.Units.SpecialCharacters;
using static Delta.Units.Systems.SI.Prefixes;

namespace Delta.Units.Systems
{
    /// <summary>
    /// Stores references to units of the SI
    /// </summary>
    public static partial class SI
    {
        public static class Prefixes
        {
            public static readonly SIPrefix yotta = new SIPrefix("yotta", "Y", 24);
            public static readonly SIPrefix zetta = new SIPrefix("zetta", "Z", 21);
            public static readonly SIPrefix exa = new SIPrefix("exa", "E", 18);
            public static readonly SIPrefix peta = new SIPrefix("peta", "P", 15);
            public static readonly SIPrefix tera = new SIPrefix("tera", "T", 9);
            public static readonly SIPrefix mega = new SIPrefix("mega", "M", 6);
            public static readonly SIPrefix kilo = new SIPrefix("kilo", "k", 3);
            public static readonly SIPrefix hecto = new SIPrefix("hecto", "h", 2);
            public static readonly SIPrefix deca = new SIPrefix("deca", "da", 1);
            public static readonly SIPrefix None = new SIPrefix(string.Empty, string.Empty, 0);
            public static readonly SIPrefix deci = new SIPrefix("deci", "d", -1);
            public static readonly SIPrefix centi = new SIPrefix("centi", "c", -2);
            public static readonly SIPrefix milli = new SIPrefix("milli", "m", -3);
            public static readonly SIPrefix micro = new SIPrefix("micro", mu, -6);
            public static readonly SIPrefix nano = new SIPrefix("nano", "n", -9);
            public static readonly SIPrefix pico = new SIPrefix("pico", "p", -12);
            public static readonly SIPrefix femto = new SIPrefix("femto", "f", -15);
            public static readonly SIPrefix atto = new SIPrefix("atto", "a", -18);
            public static readonly SIPrefix zepto = new SIPrefix("zepto", "z", -21);
            public static readonly SIPrefix yocto = new SIPrefix("yocto", "y", -24);

            internal static readonly IEnumerable<SIPrefix> All = new List<SIPrefix>
            {
                yotta, zetta, exa, peta, tera, mega, kilo, hecto, deca,
                deci, centi, milli, micro, nano, pico, femto, atto, zepto, yocto
            };
        }

        // Base units

        // We use the UK 'metre' and not the US 'meter' because this is how it is defined in SI.
        // Concerning the casing - that may seem weird for C# code, we also respect the SI convention 
        // which states a unit should be all lower case unless its name is based upon a person's 
        // (Kelvin, Celsius, Ampere, Volta...)

        public static readonly Unit metre = new Unit(nameof(metre), "m", BaseDimensions.Length);
        public static readonly Unit kilogram = new Unit(nameof(kilogram), "kg", BaseDimensions.Mass);
        public static readonly Unit second = new Unit(nameof(second), "s", BaseDimensions.Time);
        public static readonly Unit Ampere = new Unit(nameof(Ampere), "A", BaseDimensions.ElectricCurrent);
        public static readonly Unit Kelvin = new Unit(nameof(Kelvin), "K", BaseDimensions.ThermodynamicTemperature);
        public static readonly Unit mole = new Unit(nameof(mole), "mol", BaseDimensions.AmountOfSubstance);
        public static readonly Unit candela = new Unit(nameof(candela), "cd", BaseDimensions.LuminousIntensity);

        // Length units (most common ones; see https://en.wikipedia.org/wiki/Metre#SI_prefixed_forms_of_metre)
        public static readonly Unit kilometre = kilo * metre;
        public static readonly Unit centimetre = centi * metre;
        public static readonly Unit millimetre = milli * metre;
        public static readonly Unit micrometre = micro * metre;
        public static readonly Unit nanometre = nano * metre;

        // Mass units (most common ones; see https://en.wikipedia.org/wiki/Kilogram#SI_multiples)
        // Weird, but the base unit is kg, not g. Hence, we use the prefix syntax only for subdivisions of g
        // We also add the Tonne as an alias for the megagram
        public static readonly Unit gram = new Unit(nameof(gram), "g", kilogram, 1m / 1000m);
        public static readonly Unit milligram = milli * gram;
        public static readonly Unit microgram = micro * gram;
        public static readonly Unit nanogram = pico * gram;
        public static readonly Unit picogram = nano * gram;
        public static readonly Unit tonne = new Unit(nameof(tonne), "t", mega * gram);

        // Time units (extended to minute and day + common ones: see https://en.wikipedia.org/wiki/Second#SI_multiples)
        public static readonly Unit minute = new Unit("minute", "mn", second, 60m);
        public static readonly Unit hour = new Unit("hour", "h", second, 3600m);
        public static readonly Unit day = new Unit("day", "d", hour, 24m);
        public static readonly Unit millisecond = milli * second;
        public static readonly Unit microsecond = micro * second;
        public static readonly Unit nanosecond = nano * second;

        // Electric current
        public static readonly Unit milliAmpere = milli * Ampere;
        public static readonly Unit microAmpere = micro * Ampere;

        // Temperature. Although not part of the SI base units Fahrenheit and Celsius are provided here. (Celsius is considered a derived SI unit).
        public static readonly Unit Celsius = new Unit(nameof(Celsius), "°C", Kelvin,
            c => c + 273.15m, k => k - 273.15m);
        public static readonly Unit Fahrenheit = new Unit(nameof(Fahrenheit), "°F", Kelvin,
            f => (f + 459.67m) * 5m / 9m, k => 9m / 5m * k - 459.67m);

        private static string _(string name) => name.Replace('_', ' ');
    }
}
