using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class SimpleUnitTests
    {
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
            var cm = new Unit("centimetre", "cm", m, 0.01m);

            var valueInCentimetres = 25m;
            var expectedValueInMetres = 0.25m;
            var actualValueInMetres = cm.ConvertTo(valueInCentimetres, m);
            Assert.Equal(expectedValueInMetres, actualValueInMetres);

            var valueInMetres = 2.5m;
            var expectedValueInCentimetres = 250m;
            var actualValueInCentimetres = m.ConvertTo(valueInMetres, cm);
            Assert.Equal(expectedValueInCentimetres, actualValueInCentimetres);
        }

        [Fact]
        public void SimpleDerivedUnitsLevel1()
        {
            var m = new Unit("metre", "m", BaseDimensions.Length);
            var cm = new Unit("centimetre", "cm", m, x => 0.01m * x, x => 100m * x);

            var valueInCentimetres = 25m;
            var expectedValueInMetres = 0.25m;
            var actualValueInMetres = cm.ConvertTo(valueInCentimetres, m);
            Assert.Equal(expectedValueInMetres, actualValueInMetres);

            var valueInMetres = 2.5m;
            var expectedValueInCentimetres = 250m;
            var actualValueInCentimetres = m.ConvertTo(valueInMetres, cm);
            Assert.Equal(expectedValueInCentimetres, actualValueInCentimetres);
        }

        [Fact]
        public void SimpleDerivedUnitsLevel2()
        {
            var m = new Unit("metre", "m", BaseDimensions.Length);
            var cm = new Unit("centimetre", "cm", m, x => x / 100m, x => x * 100m);
            var inch = new Unit("inch", "'", cm, x => x * 2.54m, x => x / 2.54m);

            var valueInCentimetres = 254m;
            var expectedValueInInches = 100m;
            var actualValueInInches = cm.ConvertTo(valueInCentimetres, inch);
            Assert.Equal(expectedValueInInches, actualValueInInches);

            var valueInInches = 3.5m;
            var expectedValueInCentimetres = 3.5m * 2.54m;
            var actualValueInCentimetres = inch.ConvertTo(valueInInches, cm);
            Assert.Equal(expectedValueInCentimetres, actualValueInCentimetres);
        }

        [Fact]
        public void SimpleDerivedUnitsLevel2Temperatures()
        {
            var kelvin = new Unit("kelvin", "K", BaseDimensions.ThermodynamicTemperature);
            var celsius = new Unit("celsius", "°C", kelvin, c => c + 273.15m, k => k - 273.15m);
            var fahrenheit = new Unit("fahrenheit", "°F", celsius,
                f => (f - 32m) * (5m / 9m),
                c => 9m / 5m * c + 32m);

            // K -> °C and °F
            var t1InK = 320.5m;
            var expectedT1InC = 47.35m;
            var actualT1InC = kelvin.ConvertTo(t1InK, celsius);
            var expectedT1InF = 117.23m;
            var actualT1InF = kelvin.ConvertTo(t1InK, fahrenheit);

            Assert.Equal(expectedT1InC, actualT1InC);
            Assert.Equal(expectedT1InF, actualT1InF);

            // °C -> K, °F
            var t2InC = -25.3m; // Cold!!!
            var expectedT2InK = 247.85m;
            var acualT2InK = celsius.ConvertTo(t2InC, kelvin);
            var expectedT2InF = -13.54m;
            var acualT2InF = celsius.ConvertTo(t2InC, fahrenheit); 

            Assert.Equal(expectedT2InK, acualT2InK);
            Assert.Equal(expectedT2InF, acualT2InF);

            // °F -> K, °C
            var t3InF = 101.3m; // Warm!!!
            var expectedT3InK = 311.65m;
            var actualT3InK = fahrenheit.ConvertTo(t3InF, kelvin);
            var expectedT3InC = 38.5m;
            var actualT3InC = fahrenheit.ConvertTo(t3InF, celsius);

            Assert.Equal(expectedT3InK, actualT3InK);
            Assert.Equal(expectedT3InC, actualT3InC);
        }
    }
}
