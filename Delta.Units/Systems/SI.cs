namespace Delta.Units.Systems
{
    /// <summary>
    /// Stores references to units of the SI
    /// </summary>
    public static class SI
    {
        // We use the UK 'metre' and not the US 'meter' because this is how it is defined in SI.
        public static readonly Unit Metre = new Unit("metre", "m", BaseDimensions.Length); 
    }
}
