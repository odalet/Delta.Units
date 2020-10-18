namespace Delta.Units.Systems
{
    partial class SI
    {
        /// <summary>
        /// SI-derived area units
        /// </summary>
        public static class Area
        {
#pragma warning disable CS1591
            public static readonly Unit square_metre = new Unit(_(nameof(square_metre)), "m²", metre * metre);
            public static readonly Unit square_centimetre = new Unit(_(nameof(square_centimetre)), "cm²", centimetre * centimetre);
            public static readonly Unit square_millimetre = new Unit(_(nameof(square_millimetre)), "mm²", millimetre * millimetre);
            public static readonly Unit square_micrometre = new Unit(_(nameof(square_micrometre)), SpecialCharacters.mu + "m²", micrometre * micrometre);
            public static readonly Unit square_kilometre = new Unit(_(nameof(square_kilometre)), "km²", kilometre * kilometre);

            public static readonly Unit hectare = new Unit(nameof(hectare), "ha", metre * metre, 10000m);
            public static readonly Unit are = new Unit(nameof(are), "a", metre * metre, 100m);
#pragma warning restore CS1591
        }
    }
}
