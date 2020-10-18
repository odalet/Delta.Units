using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using static Delta.Units.DimensionFormulaIndices;

namespace Delta.Units
{
    [ExcludeFromCodeCoverage]
    public class DimensionTests
    {
        private readonly ITestOutputHelper output;

        public DimensionTests(ITestOutputHelper helper) => output = helper;

        [Fact]
        public void Pow()
        {
            var length = BaseDimensions.Length;
            var surface1 = length * length;
            var surface2 = length ^ 2;

            output.WriteLine($"Length: {length}");
            output.WriteLine($"Surface 1: {surface1}"); // L.L
            output.WriteLine($"Surface 2: {surface2}"); // L^2
            
            Assert.True(surface1.Formula[l] == 2);
            Assert.True(surface2.Formula[l] == 2);

            Assert.Equal("L^2", surface1.GetFormulaAsString());
            Assert.Equal("L^2", surface1.GetFormulaAsString());
        }

        [Fact]
        public void NonePow()
        {
            var none = BaseDimensions.None;
            var none1 = none * none;
            var none2 = none ^ 5;

            var nil = (Dimension)null;
            var none3 = nil ^ 4;

            output.WriteLine($"None: {none}"); // ""
            output.WriteLine($"None 1: {none1}"); // ""
            output.WriteLine($"None 2: {none2}"); // ""
            output.WriteLine($"None 3: {none3}"); // ""

            Assert.True(none1.IsNone());
            Assert.True(none2.IsNone());
            Assert.True(none3.IsNone());

            Assert.Equal("Z", none1.GetFormulaAsString());
            Assert.Equal("Z", none2.GetFormulaAsString());
            Assert.Equal("Z", none3.GetFormulaAsString());

            Assert.Equal(string.Empty, none1.Symbol);
            Assert.Equal(string.Empty, none2.Symbol);
            Assert.Equal(string.Empty, none3.Symbol);
            
            Assert.Equal(string.Empty, none1.Name);
            Assert.Equal(string.Empty, none2.Name);
            Assert.Equal(string.Empty, none3.Name);

            Assert.Equal(string.Empty, none1.ToString());
            Assert.Equal(string.Empty, none2.ToString());
            Assert.Equal(string.Empty, none3.ToString());
        }

        [Fact]
        public void MultiplyByNone()
        {
            var length = BaseDimensions.Length;
            var surface1 = length ^ 2;
            var surface2 = surface1 * BaseDimensions.None;
            var surface3 = surface1 * (Dimension)null;
            var surface4 = (Dimension)null * surface1;

            output.WriteLine($"Length: {length.GetFormulaAsString()}");
            output.WriteLine($"Surface 1: {surface1.GetFormulaAsString()}"); // L^2
            output.WriteLine($"Surface 2: {surface2.GetFormulaAsString()}"); // L^2
            output.WriteLine($"Surface 3: {surface3.GetFormulaAsString()}"); // L^2
            output.WriteLine($"Surface 4: {surface4.GetFormulaAsString()}"); // L^2

            Assert.True(surface1.Formula[l] == 2);
            Assert.True(surface2.Formula[l] == 2);
            Assert.True(surface3.Formula[l] == 2);
            Assert.True(surface4.Formula[l] == 2);
        }

        [Fact]
        public void DivideByNone()
        {
            var length = BaseDimensions.Length;
            var surface1 = length ^ 2;
            var surface2 = surface1 / BaseDimensions.None;
            var surface3 = surface1 / null;
            var surface4 = BaseDimensions.None / surface1;
            var surface5 = null / surface1;

            output.WriteLine($"Length: {length.GetFormulaAsString()}");
            output.WriteLine($"Surface 1: {surface1.GetFormulaAsString()}"); // L^2
            output.WriteLine($"Surface 2: {surface2.GetFormulaAsString()}"); // L^2
            output.WriteLine($"Surface 3: {surface3.GetFormulaAsString()}"); // L^2
            output.WriteLine($"Surface 4: {surface4.GetFormulaAsString()}"); // L^-2
            output.WriteLine($"Surface 5: {surface5.GetFormulaAsString()}"); // L^-2

            Assert.True(surface1.Formula[l] == 2);
            Assert.True(surface2.Formula[l] == 2);
            Assert.True(surface3.Formula[l] == 2);
            Assert.True(surface4.Formula[l] == -2);
            Assert.True(surface5.Formula[l] == -2);
        }

        [Fact]
        public void CombineWithNoneToString()
        {
            var length = BaseDimensions.Length;
            var surface1 = length ^ 2;
            var surface2 = surface1 * BaseDimensions.None;
            var surface3 = surface1 * (Dimension)null;
            var surface4 = (Dimension)null * surface1;

            var expectedSymbol = "L^2";
            Assert.Equal(surface1.Symbol, expectedSymbol);
            Assert.Equal(surface2.Symbol, expectedSymbol);
            Assert.Equal(surface3.Symbol, expectedSymbol);
            Assert.Equal(surface4.Symbol, expectedSymbol);

            var expectedName = $"{length.Name} ^ 2";
            Assert.Equal(surface1.Name, expectedName);
            Assert.Equal(surface2.Name, expectedName);
            Assert.Equal(surface3.Name, expectedName);
            Assert.Equal(surface4.Name, expectedName);
        }

        [Fact]
        public void MixedDimensions()
        {
            var velocity1 = BaseDimensions.Length / BaseDimensions.Time;
            var velocity2 = BaseDimensions.Length * (BaseDimensions.Time ^ -1); // Beware, ^ priority is not the mathematical one!

            var expectedFormula = "L.T^-1";
            var expectedSymbol1 = "L/T";
            var expectedSymbol2 = expectedFormula;

            Assert.Equal(expectedFormula, velocity1.GetFormulaAsString());
            Assert.Equal(expectedFormula, velocity2.GetFormulaAsString());

            Assert.Equal(expectedSymbol1, velocity1.Symbol);
            Assert.Equal(expectedSymbol2, velocity2.Symbol);
        }

        [Fact]
        public void DimensionsEquality()
        {
            var velocity1 = BaseDimensions.Length / BaseDimensions.Time;
            var velocity2 = BaseDimensions.Length * (BaseDimensions.Time ^ -1); // Beware, ^ priority is not the mathematical one!

            var alias = velocity1;

            Assert.Equal(velocity1, velocity2);
            Assert.True(velocity1 == velocity2);
            Assert.True(velocity1.Equals(velocity2));
            Assert.True(velocity2.Equals(velocity1));
            Assert.Equal(velocity1.GetHashCode(), velocity2.GetHashCode());

            Assert.Equal(velocity1, alias);
            Assert.True(velocity1 == alias);
            Assert.True(velocity1.Equals(alias));
            Assert.Equal(velocity1.GetHashCode(), alias.GetHashCode());

            Assert.NotEqual(velocity1, BaseDimensions.ElectricCurrent);
            Assert.True(velocity1 != BaseDimensions.ElectricCurrent);
            Assert.True(!velocity1.Equals(BaseDimensions.ElectricCurrent));
            Assert.False(velocity1.Equals(BaseDimensions.ElectricCurrent));

            Assert.NotEqual(velocity1.GetHashCode(), BaseDimensions.ElectricCurrent.GetHashCode());
        }

        [Fact]
        public void NoneDimensionsEquality()
        {
            var none1 = BaseDimensions.None;
            var none2 = BaseDimensions.Mass / BaseDimensions.Mass;
            var nil = (Dimension)null;

            Assert.Equal(none1, none2);

            // This does not work
            Assert.NotEqual(nil, none1); 
            Assert.NotEqual(nil, none2);
            Assert.NotNull(none1);
            Assert.NotNull(none2);

            Assert.True(none1 == none2);
            Assert.True(none1 == null);
            Assert.True(none2 == null);
            Assert.True(null == none1);
            Assert.True(null == none2);

            Assert.False(none1 is null);
            Assert.False(none2 is null);
            Assert.True(nil is null);

            Assert.True(none1.Equals(none2));
            Assert.True(none1.Equals(null));
            Assert.True(none2.Equals(null));

            _ = Assert.Throws<NullReferenceException>(() => { try { _ = ((Dimension)null).Equals(none1); } catch { throw; } });
            _ = Assert.Throws<NullReferenceException>(() => { try { _ = ((Dimension)null).Equals(none2); } catch { throw; } });
        }

        [Fact]
        public void TestBaseDimensionsCount()
        {
            var count = 0;
            var properties = typeof(BaseDimensions).GetProperties(BindingFlags.Public | BindingFlags.Static).Where(pi => pi.PropertyType == typeof(Dimension));
            foreach (var pi in properties)
            {
                var value = pi.GetGetMethod().Invoke(null, null);
                output.WriteLine(value.ToString());
                count++;
            }

            Assert.Equal(BaseDimensions.Count, count);
        }

        [Fact]
        public void TestFormulaAsString()
        {
            var length = BaseDimensions.Length;
            var time = BaseDimensions.Time;
            var nil = (Dimension)null;
            var none = BaseDimensions.None;
            var velocity = length / time;

            Assert.Equal("L", length.GetFormulaAsString());
            Assert.Equal("T", time.GetFormulaAsString());
            Assert.Equal("L^2", (length * length).GetFormulaAsString());
            Assert.Equal("Z", (time / time).GetFormulaAsString());
            Assert.Equal("Z", nil.GetFormulaAsString());
            Assert.Equal("Z", none.GetFormulaAsString());
            Assert.Equal("L.T^-1", velocity.GetFormulaAsString());
        }

        [Fact]
        public void TestDimensionFormula()
        {
            // Well... This test is mostly here for code coverage

            var lFormula = BaseDimensions.Length.Formula;
            var mFormula = BaseDimensions.Mass.Formula;
            var tFormula = BaseDimensions.Time.Formula;
            var iFormula = BaseDimensions.ElectricCurrent.Formula;
            var thFormula = BaseDimensions.ThermodynamicTemperature.Formula;
            var nFormula = BaseDimensions.AmountOfSubstance.Formula;
            var jFormula = BaseDimensions.LuminousIntensity.Formula;
            var noneFormula = BaseDimensions.None.Formula;

            Assert.Equal(1, lFormula.L);
            Assert.Equal(1, mFormula.M);
            Assert.Equal(1, tFormula.T);
            Assert.Equal(1, iFormula.I);
            Assert.Equal(1, thFormula.Th);
            Assert.Equal(1, nFormula.N);
            Assert.Equal(1, jFormula.J);
            Assert.Equal(1, noneFormula.Z);

            var enumerator = ((System.Collections.IEnumerable)noneFormula).GetEnumerator();            
            var value = 0;
            while (enumerator.MoveNext())
            {
                Assert.True(value == 0);
                value = (int)enumerator.Current;
            }

            Assert.True(value != 0);

            var alias = lFormula;
            Assert.True(alias.Equals(lFormula));
            Assert.True(lFormula.Equals(alias));
            Assert.True(noneFormula.Equals(null));
        }
    }
}
