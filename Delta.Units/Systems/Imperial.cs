namespace Delta.Units.Systems
{
    /// <summary>
    /// Stores references to Imperial units (see https://en.wikipedia.org/wiki/Imperial_units)
    /// </summary>
    /// <remarks>
    /// The units here are defined either against <see cref="SI"/> units or against other <see cref="Imperial"/> units.
    /// </remarks>
    public static class Imperial
    {
        ///////////////////////////////////        
        // Length units

        public static readonly Unit thou = new Unit(_(nameof(thou)), "th", SI.metre, 0.0000254m);
        public static readonly Unit inch = new Unit(_(nameof(inch)), "in", thou, 1000m);
        public static readonly Unit foot = new Unit(_(nameof(foot)), "ft", inch, 12m);
        public static readonly Unit yard = new Unit(_(nameof(yard)), "yd", foot, 3m);
        public static readonly Unit chain = new Unit(_(nameof(chain)), "ch", yard, 22m);
        public static readonly Unit furlong = new Unit(_(nameof(furlong)), "fur", chain, 10m);
        public static readonly Unit mile = new Unit(_(nameof(mile)), "mi", furlong, 8m);
        public static readonly Unit league = new Unit(_(nameof(league)), "lea", mile, 3m);

        // Gunter's survey units
        public static readonly Unit link = new Unit(_(nameof(link)), _(nameof(link)), inch, 7.92m);
        public static readonly Unit rod = new Unit(_(nameof(rod)), _(nameof(rod)), link, 25m);
        
        // Maritime Length units
        public static readonly Unit fathom = new Unit(_(nameof(fathom)), "ftm", SI.metre, 1.852m);
        public static readonly Unit cable = new Unit(_(nameof(cable)), _(nameof(cable)), fathom, 100m);
        public static readonly Unit nautical_mile = new Unit(_(nameof(nautical_mile)), _(nameof(nautical_mile)), cable, 10m);

        ///////////////////////////////////        
        // Area units

        public static readonly Unit perch = new Unit(_(nameof(perch)), _(nameof(perch)), rod * rod);
        public static readonly Unit rood = new Unit(_(nameof(rood)), _(nameof(rood)), furlong * rod);
        public static readonly Unit acre = new Unit(_(nameof(acre)), _(nameof(acre)), furlong * chain);

        public static readonly Unit square_yard = new Unit(_(nameof(square_yard)), "yd²", yard * yard);
        public static readonly Unit square_mile = new Unit(_(nameof(square_mile)), "mi²", mile * mile);

        ///////////////////////////////////        
        // Volume units

        public static readonly Unit fluid_ounce = new Unit(_(nameof(fluid_ounce)),"fl oz", SI.Volume.millilitre, 28.4130625m);
        public static readonly Unit ounce = new Unit(_(nameof(ounce)),"oz", fluid_ounce); // alias of fl oz
        public static readonly Unit gill = new Unit(_(nameof(gill)),"gi", fluid_ounce, 5m);
        public static readonly Unit pint = new Unit(_(nameof(pint)), "pt", fluid_ounce, 20m);
        public static readonly Unit quart = new Unit(_(nameof(quart)), "qt", fluid_ounce, 40m);
        public static readonly Unit gallon = new Unit(_(nameof(gallon)), "gal", fluid_ounce, 160m);

        public static readonly Unit cubic_inch = new Unit(_(nameof(cubic_inch)), "in" + SpecialCharacters.cubic, inch ^ 3);

        private static string _(string name) => name.Replace('_', ' ');
    }
}
