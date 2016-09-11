namespace Delta.Units.Systems
{
    /// <summary>
    /// Common units aliased by their symbol.
    /// </summary>
    /// <remarks>
    /// By inserting a <code>using static Delta.Units.Systems.Aliases</code> at then top of a source file,
    /// the units can be used without any prefix.
    /// </remarks>
    public static class Aliases
    {
        /// <summary>Aliases <see cref="SI.metre"/></summary>
        public static readonly Unit m = SI.metre;
        /// <summary>Aliases <see cref="SI.centimetre"/></summary>
        public static readonly Unit cm = SI.centimetre;
        /// <summary>Aliases <see cref="SI.millimetre"/></summary>
        public static readonly Unit mm = SI.millimetre;
        /// <summary>Aliases <see cref="SI.micrometre"/></summary>
        public static readonly Unit micron = SI.micrometre;
        /// <summary>Aliases <see cref="SI.kilometre"/></summary>
        public static readonly Unit km = SI.kilometre;

        /// <summary>Aliases <see cref="SI.millisecond"/></summary>
        public static readonly Unit ms = SI.millisecond;
        /// <summary>Aliases <see cref="SI.second"/></summary>
        public static readonly Unit s = SI.second;
        /// <summary>Aliases <see cref="SI.minute"/></summary>
        public static readonly Unit mn = SI.minute;
        /// <summary>Aliases <see cref="SI.hour"/></summary>
        public static readonly Unit h = SI.hour;

        /// <summary>Aliases <see cref="SI.gram"/></summary>
        public static readonly Unit g = SI.gram;
        /// <summary>Aliases <see cref="SI.kilogram"/></summary>
        public static readonly Unit kg = SI.kilogram;
        /// <summary>Aliases <see cref="SI.milligram"/></summary>
        public static readonly Unit mg = SI.milligram;
        /// <summary>Aliases <see cref="SI.v"/></summary>
        public static readonly Unit t = SI.tonne;

        /// <summary>Aliases <see cref="SI.Ampere"/></summary>
        public static readonly Unit A = SI.Ampere;
        /// <summary>Aliases <see cref="SI.milliAmpere"/></summary>
        public static readonly Unit mA = SI.milliAmpere;

        /// <summary>Aliases <see cref="SI.Kelvin"/></summary>
        public static readonly Unit K = SI.Kelvin;
        /// <summary>Aliases <see cref="SI.Celsius"/></summary>
        public static readonly Unit C = SI.Celsius;
        /// <summary>Aliases <see cref="SI.Fahrenheit"/></summary>
        public static readonly Unit F = SI.Fahrenheit;

        /// <summary>Aliases <see cref="SI.Area.square_metre"/></summary>
        public static readonly Unit sqm = SI.Area.square_metre;
        /// <summary>Aliases <see cref="SI.Area.square_centimetre"/></summary>
        public static readonly Unit sqcm = SI.Area.square_centimetre;
        /// <summary>Aliases <see cref="SI.Area.square_millimetre"/></summary>
        public static readonly Unit sqmm = SI.Area.square_millimetre;
        /// <summary>Aliases <see cref="SI.Area.square_kilometre"/></summary>
        public static readonly Unit sqkm = SI.Area.square_kilometre;

        /// <summary>Aliases <see cref="SI.Volume.cubic_metre"/></summary>
        public static readonly Unit ccm = SI.Volume.cubic_metre;
        /// <summary>Aliases <see cref="SI.Volume.cubic_centimetre"/></summary>
        public static readonly Unit cccm = SI.Volume.cubic_centimetre;
        /// <summary>Aliases <see cref="SI.Volume.cubic_millimetre"/></summary>
        public static readonly Unit ccmm = SI.Volume.cubic_millimetre;

        /// <summary>Aliases <see cref="SI.Volume.litre"/></summary>
        public static readonly Unit L = SI.Volume.litre;
        /// <summary>Aliases <see cref="SI.Volume.centilitre"/></summary>
        public static readonly Unit cL = SI.Volume.centilitre;
        /// <summary>Aliases <see cref="SI.Volume.millilitre"/></summary>
        public static readonly Unit mL = SI.Volume.millilitre;

        /// <summary>Aliases <see cref="SI.Velocity.metre_per_second"/></summary>
        public static readonly Unit mps = SI.Velocity.metre_per_second;
        /// <summary>Aliases <see cref="SI.Velocity.kilometre_per_hour"/></summary>
        public static readonly Unit kmph = SI.Velocity.kilometre_per_hour;

        /// <summary>Aliases <see cref="Dimensionless.radian"/></summary>
        public static readonly Unit rad = Dimensionless.radian;
        /// <summary>Aliases <see cref="Dimensionless.degree"/></summary>
        public static readonly Unit deg = Dimensionless.degree;
        /// <summary>Aliases <see cref="Dimensionless.turn"/></summary>
        public static readonly Unit tr = Dimensionless.turn;

        /// <summary>Aliases <see cref="Dimensionless.parts_per_million"/></summary>
        public static readonly Unit ppm = Dimensionless.parts_per_million;
    }
}
