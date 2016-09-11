using System.Linq;

namespace Delta.Units
{
    public sealed class Dimension
    {
        // For now, only this assembly is allowed to create dimensions
        internal Dimension(string name, string symbol, DimensionFormula formula)
        {
            Name = name;
            Symbol = symbol;
            Formula = formula.All(exp => exp == 0) ? DimensionFormula.None : formula;
        }

        public string Name { get; }
        public string Symbol { get; }
        internal DimensionFormula Formula { get; }

        #region Operations on Dimensions

        public Dimension Pow(int exponent)
        {
            var newName = this.IsNone() ? string.Empty : $"{Name} ^ {exponent}";
            var newSymbol = this.IsNone() ? string.Empty : $"{Symbol}^{exponent}";

            var newFormula = Formula.Clone();
            for (var index = 0; index < newFormula.Count; index++)
                newFormula[index] *= exponent;
            
            return new Dimension(newName, newSymbol, newFormula);
        }
        
        public Dimension MultiplyBy(Dimension other)
        {
            var operand = other ?? BaseDimensions.None;

            var newName = Helpers.CreateDimensionName(this, other, "*");
            var newSymbol = Helpers.CreateDimensionSymbol(this, other, ".");

            var newFormula = Formula.Clone();
            for (var index = 0; index < newFormula.Count; index++)
                newFormula[index] += operand.Formula[index];
            
            return new Dimension(newName, newSymbol, newFormula);
        }

        public Dimension DivideBy(Dimension other)
        {
            var operand = other ?? BaseDimensions.None;

            var newName = Helpers.CreateDimensionName(this, other, "/");
            var newSymbol = Helpers.CreateDimensionSymbol(this, other, "/");

            var newFormula = Formula.Clone();
            for (var index = 0; index < newFormula.Count; index++)
                newFormula[index] -= operand.Formula[index];
            
            return new Dimension(newName, newSymbol, newFormula);
        }

        #endregion

        #region Comparison, Equality

        public override bool Equals(object obj)
        {
            var other = obj as Dimension;
            if (ReferenceEquals(other, null)) // Considered to be the 'None' dimension
                return Formula.IsNone();

            if (ReferenceEquals(this, other)) return true;

            return Formula.Equals(other.Formula);
        }

        public override int GetHashCode() => Formula.GetHashCode();

        #endregion

        #region Operators overloads

        public static Dimension operator *(Dimension left, Dimension right) =>
            (left ?? BaseDimensions.None).MultiplyBy(right);

        public static Dimension operator /(Dimension left, Dimension right) =>
            (left ?? BaseDimensions.None).DivideBy(right);

        // Beware, ^ priority is not the mathematical one! It should always be used with parens to avoid mistakes
        // This is because, natively, ^ is the XOR operator, not the pow one... I miss an ** operator in C# 
        public static Dimension operator ^(Dimension dimension, int exponent) =>
            (dimension ?? BaseDimensions.None).Pow(exponent);

        public static bool operator ==(Dimension left, Dimension right) =>
            (left ?? BaseDimensions.None).Equals(right);

        public static bool operator !=(Dimension left, Dimension right) =>
            !(left == right);

        #endregion

        public override string ToString() => this.IsNone() ? string.Empty : $"{Symbol} ({Name})";
    }
}
