using System;

namespace Delta.Units.Systems
{
    /// <summary>
    /// This class defines a few dimension-less units such as angles and proportions
    /// </summary>
    public static class Dimensionless
    {
#pragma warning disable CS1591
        // Not an angle, but a useful value
        public static readonly decimal PI = Helpers.PI;

        // Angles, based on radian

        public static readonly Unit radian = new Unit(nameof(radian), "rad", SI.metre / SI.metre); // SI defines radian as m/m
        public static readonly Unit degree = new Unit(nameof(degree), "°", radian, PI / 180m);
        public static readonly Unit turn = new Unit(nameof(turn), "tr", radian, 2m * PI); // See https://en.wikipedia.org/wiki/Turn_(geometry)

        // Proportions

        // I don't know if this exists, but it is defined here to act as a base unit. I suppose that using Amount of substance
        // as base dimension makes sense...
        private static readonly Unit unit = new Unit(nameof(unit), "u", BaseDimensions.AmountOfSubstance / BaseDimensions.AmountOfSubstance);
        public static readonly Unit percent = new Unit(nameof(percent), "%", unit, 1m / 100m);
        public static readonly Unit permille = new Unit(nameof(permille), SpecialCharacters.permille, unit, 1m / 1000m);
        public static readonly Unit parts_per_million = new Unit(_(nameof(parts_per_million)), "ppm", unit, 1m / 1000000m);

#pragma warning restore CS1591

        private static string _(string name) => name.Replace('_', ' ');
    }
}
