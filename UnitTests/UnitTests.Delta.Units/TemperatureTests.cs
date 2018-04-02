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
            var c = 0m * Celsius;
            var k = c.ConvertTo(Kelvin);
            Assert.Equal(273.15m, k.Value);
        }

        [Fact]
        public void ZeroKelvinIsMinus273Dot15Celsius()
        {
            var k = 0m * Kelvin;
            var c = k.ConvertTo(Celsius);
            Assert.Equal(-273.15m, c.Value);
        }

        [Fact]
        public void OneCelsiusIs33Dot8Fahrenheit()
        {
            var f = 33.8m * Fahrenheit;
            var c = f.ConvertTo(Celsius);

            Assert.Equal(1m, c.Value);
        }

        [Fact]
        public void OneFahrenheitIsMinus17Dot222222Celsius()
        {
            var c = -17.2222222m * Celsius;
            var f = c.ConvertTo(Fahrenheit);

            // Here we have a rounding error because, the initial c value is not exact
            Assert.Equal(1m, Math.Round(f.Value, 5));
        }
    }
}
