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

        private static string _(string name) => name.Replace('_', ' ');
    }
}
