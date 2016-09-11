namespace Delta.Units.Systems
{
    partial class SI
    {
        /// <summary>
        /// SI-derived speed units
        /// </summary>
        public static class Velocity
        {
            public static readonly Unit kilometre_per_hour = new Unit(_(nameof(kilometre_per_hour)), "km/h", kilometre / hour);
            public static readonly Unit metre_per_second = new Unit(_(nameof(metre_per_second)), "m/s", metre / second);
        }
    }
}
