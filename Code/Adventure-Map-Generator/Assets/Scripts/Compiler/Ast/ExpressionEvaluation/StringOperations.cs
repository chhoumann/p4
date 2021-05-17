using System;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public class StringOperations : Calculator<string>
    {
        public override string Add(string a, string b) => string.Concat(a, b);

        public override string Subtract(string a, string b)
        {
            throw new InvalidOperationException($"Cannot subtract string {a} from {b}.");
        }

        public override string Multiply(string a, string b)
        {
            throw new InvalidOperationException($"Cannot multiply string {a} and {b}.");
        }

        public override string Divide(string a, string b)
        {
            throw new InvalidOperationException($"Cannot divide string {a} and {b}.");
        }

        public override string GetValue(int a) => a.ToString();

        public override string GetValue(float a) => a.ToString();

        public override string GetValue(string a) => a;

        public override string GetValue(ArrayNode a)
        {
            throw new InvalidOperationException("Arrays cannot be used as strings.");
        }

        public override string GetValue(ExitValueNode a)
        {
            throw new InvalidOperationException("Exits cannot be used as strings.");
        }
    }
}