using static Delta.Units.Systems.SI.Prefixes;

namespace Delta.Units.Systems
{
    partial class SI
    {
        /// <summary>
        /// SI-derived volume units
        /// </summary>
        public static class Volume
        {
            public static readonly Unit cubic_metre = new Unit(_(nameof(cubic_metre)), "m" + SpecialCharacters.cubic, metre ^ 3);
            public static readonly Unit cubic_centimetre = new Unit(_(nameof(cubic_centimetre)), "cm" + SpecialCharacters.cubic, centimetre ^ 3);
            public static readonly Unit cubic_millimetre = new Unit(_(nameof(cubic_millimetre)), "mm" + SpecialCharacters.cubic, millimetre ^ 3);
            public static readonly Unit cubic_micrometre = new Unit(_(nameof(cubic_micrometre)), SpecialCharacters.mu + "m" + SpecialCharacters.cubic, micrometre ^ 3);

            // Here again, we prefer the UK spelling to the US as it is the SI preferance.
            public static readonly Unit litre = new Unit(nameof(litre), "L", (deci * metre) ^ 3);

            // See https://en.wikipedia.org/wiki/Litre#SI_prefixes_applied_to_the_litre
            public static readonly Unit hectolitre = hecto * litre;
            public static readonly Unit decilitre = deci * litre;
            public static readonly Unit centilitre = centi * litre;
            public static readonly Unit millilitre = milli * litre;
            public static readonly Unit microlitre = micro * litre;
        }
    }
}
