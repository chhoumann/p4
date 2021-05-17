using System;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ArrayCalculator : Calculator<ArrayNode>
    {
        public override ArrayNode Add(ArrayNode a, ArrayNode b)
        {
            throw new InvalidOperationException("Cannot add arrays.");
        }

        public override ArrayNode Subtract(ArrayNode a, ArrayNode b)
        {
            throw new InvalidOperationException("Cannot subtract arrays.");
        }

        public override ArrayNode Multiply(ArrayNode a, ArrayNode b)
        {
            throw new InvalidOperationException("Cannot multiply arrays.");
        }

        public override ArrayNode Divide(ArrayNode a, ArrayNode b)
        {
            throw new InvalidOperationException("Cannot divide arrays.");
        }

        public override ArrayNode GetValue(int a)
        {
            throw new InvalidOperationException($"Integer {a} is not an array.");
        }

        public override ArrayNode GetValue(float a)
        {
            throw new InvalidOperationException($"Float {a} is not an array.");
        }

        public override ArrayNode GetValue(string a)
        {
            throw new InvalidOperationException($"String {a} is not an array.");
        }

        public override ArrayNode GetValue(ArrayNode a) => a;

        public override ArrayNode GetValue(ExitValueNode a)
        {
            throw new InvalidOperationException("ExitValue is not an array.");
        }
    }
}