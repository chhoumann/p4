namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class IntCalculator : Calculator<int>
    {
        public override int Add(int a, int b) => a + b;

        public override int Subtract(int a, int b) => a - b;

        public override int Multiply(int a, int b) => a * b;

        public override int Divide(int a, int b) => a / b;

        public override int GetValue(int a) => a;

        public override int GetValue(float a) => (int) a;
    }
}

