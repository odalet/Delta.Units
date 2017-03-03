using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using static Delta.Units.Systems.SI;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class TemperatureTests
    {
        [Fact]
        public void ZeroCelsiusIs273Dot15Kelvin()
        {
            var c = 0.0 * Celsius;
            var k = c.ConvertTo(Kelvin);
            Assert.Equal(k.Value, 273.15);
        }

        [Fact]
        public void ZeroKelvinIsMinus273Dot15Celsius()
        {
            var k = 0.0 * Kelvin;
            var c = k.ConvertTo(Celsius);
            Assert.Equal(c.Value, -273.15);
        }

        [Fact]
        public void OneCelsiusIs33Dot8Fahrenheit()
        {
            var f = 33.8 * Fahrenheit;
            var c = f.ConvertTo(Celsius);

            // There is a rounding error, because, fahrenheit to celsius conversion goes through kelvin
            // and a 9/5 factor (which is 0.555555...). In order to have an exact result, a 'shortcut' 
            // conversion set between celsius and farhenheit should be added (something to support in V2?)
            Assert.True(NearlyEqual(c.Value, 1.0));
        }

        [Fact]
        public void OneFahrenheitIsMinus17Dot222222Celsius()
        {
            var c = -17.2222222 * Celsius;
            var f = c.ConvertTo(Fahrenheit);

            ////// There is a rounding error, because, fahrenheit to celsius conversion goes through kelvin
            ////// and a 9/5 factor (which is 0.555555...). In order to have an exact result, a 'shortcut' 
            ////// conversion set between celsius and farhenheit should be added (something to support in V2?)
            Assert.True(NearlyEqual(f.Value, 1.0));
        }

        private static bool NearlyEqual(double d1, double d2, double precision = 1000000.0) => 
            Math.Truncate(d1 * precision) == Math.Truncate(d2 * precision);
    }
}
