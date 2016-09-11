using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using Xunit.Abstractions;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class SimpleUnitTests
    {
        private readonly ITestOutputHelper output;

        public SimpleUnitTests(ITestOutputHelper helper)
        {
            output = helper;
        }

        [Fact]
        public void Compatibility()
        {
            var cm = new Unit("centimetre", "cm", BaseDimensions.Length);
            var dm = new Unit("decimetre", "dm", BaseDimensions.Length);

            Assert.True(cm.IsCompatibleWith(dm));
            Assert.True(dm.IsCompatibleWith(cm));
            Assert.True(Unit.AreCompatible(cm, dm));
            Assert.True(Unit.AreCompatible(dm, cm));
        }

        [Fact]
        public void NoneCompatibility()
        {
            var none = Unit.None;
            var nil = (Unit)null;

            Assert.True(nil.IsCompatibleWith(none));
            Assert.True(none.IsCompatibleWith(nil));
            Assert.True(Unit.AreCompatible(nil, none));
            Assert.True(Unit.AreCompatible(none, nil));
        }

        [Fact]
        public void MultipliedUnit()
        {
            var m = new Unit("metre", "m", BaseDimensions.Length);
            var cm = new Unit("centimetre", "cm", m, 0.01);

            var valueInCentimetres = 25.0;
            var expectedValueInMetres = 0.25;
            var actualValueInMetres = cm.ConvertTo(valueInCentimetres, m);
            Assert.Equal(expectedValueInMetres, actualValueInMetres);

            var valueInMetres = 2.5;
            var expectedValueInCentimetres = 250.0;
            var actualValueInCentimetres = m.ConvertTo(valueInMetres, cm);
            Assert.Equal(expectedValueInCentimetres, actualValueInCentimetres);
        }

        [Fact]
        public void SimpleDerivedUnitsLevel1()
        {
            var m = new Unit("metre", "m", BaseDimensions.Length);
            var cm = new Unit("centimetre", "cm", m, x => 0.01 * x, x => 100.0 * x);

            var valueInCentimetres = 25.0;
            var expectedValueInMetres = 0.25;
            var actualValueInMetres = cm.ConvertTo(valueInCentimetres, m);
            Assert.Equal(expectedValueInMetres, actualValueInMetres);

            var valueInMetres = 2.5;
            var expectedValueInCentimetres = 250.0;
            var actualValueInCentimetres = m.ConvertTo(valueInMetres, cm);
            Assert.Equal(expectedValueInCentimetres, actualValueInCentimetres);
        }

        [Fact]
        public void SimpleDerivedUnitsLevel2()
        {
            var m = new Unit("metre", "m", BaseDimensions.Length);
            var cm = new Unit("centimetre", "cm", m, x => x / 100.0, x => x * 100.0);
            var inch = new Unit("inch", "'", cm, x => x * 2.54, x => x / 2.54);

            var valueInCentimetres = 254.0;
            var expectedValueInInches = 100.0;
            var actualValueInInches = cm.ConvertTo(valueInCentimetres, inch);
            Assert.Equal(expectedValueInInches, actualValueInInches);

            var valueInInches = 3.5;
            var expectedValueInCentimetres = 3.5 * 2.54;
            var actualValueInCentimetres = inch.ConvertTo(valueInInches, cm);
            Assert.Equal(expectedValueInCentimetres, actualValueInCentimetres);
        }
        
        [Fact]
        public void SimpleDerivedUnitsLevel2Temperatures()
        {
            var kelvin = new Unit("kelvin", "K", BaseDimensions.ThermodynamicTemperature);
            var celsius = new Unit("celsius", "°C", kelvin, c => c + 273.15, k => k - 273.15);
            var fahrenheit = new Unit("fahrenheit", "°F", celsius,
                f => (f - 32.0) * (5.0 / 9.0),
                c => (9.0 / 5.0) * c + 32.0);

            // K -> °C and °F
            var t1InK = 320.5;
            var expectedT1InC = 47.35;
            var actualT1InC = Math.Round(kelvin.ConvertTo(t1InK, celsius), 2);
            var expectedT1InF = 117.23;
            var actualT1InF = Math.Round(kelvin.ConvertTo(t1InK, fahrenheit), 2);

            Assert.Equal(expectedT1InC, actualT1InC);
            Assert.Equal(expectedT1InF, actualT1InF);

            // °C -> K, °F
            var t2InC = -25.3; // Cold!!!
            var expectedT2InK = 247.85;
            var acualT2InK = Math.Round(celsius.ConvertTo(t2InC, kelvin), 2);
            var expectedT2InF = -13.54;
            var acualT2InF = Math.Round(celsius.ConvertTo(t2InC, fahrenheit), 2);

            Assert.Equal(expectedT2InK, acualT2InK);
            Assert.Equal(expectedT2InF, acualT2InF);

            // °F -> K, °C
            var t3InF = 101.3; // Warm!!!
            var expectedT3InK = 311.65;
            var actualT3InK = Math.Round(fahrenheit.ConvertTo(t3InF, kelvin), 2);
            var expectedT3InC = 38.5;
            var actualT3InC = Math.Round(fahrenheit.ConvertTo(t3InF, celsius), 2);

            Assert.Equal(expectedT3InK, actualT3InK);
            Assert.Equal(expectedT3InC, actualT3InC);
        }
    }
}
