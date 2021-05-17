namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class FloatCalculator : Calculator<float>
    {
        public override float Add(float a, float b) => a + b;

        public override float Subtract(float a, float b) => a - b;

        public override float Multiply(float a, float b) => a * b;

        public override float Divide(float a, float b) => a / b;

        public override float GetValue(float a) => a;

        public override float GetValue(int a) => a;
    }
}