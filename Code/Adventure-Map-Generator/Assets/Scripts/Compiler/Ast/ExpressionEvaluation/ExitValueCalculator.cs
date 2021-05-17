using System;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ExitValueCalculator : Calculator<ExitValueNode>
    {
        public override ExitValueNode Add(ExitValueNode a, ExitValueNode b)
        {
            throw new InvalidOperationException("Cannot add exits.");
        }

        public override ExitValueNode Subtract(ExitValueNode a, ExitValueNode b)
        {
            throw new InvalidOperationException("Cannot subtract exits.");
        }

        public override ExitValueNode Multiply(ExitValueNode a, ExitValueNode b)
        {
            throw new InvalidOperationException("Cannot multiply exits.");
        }

        public override ExitValueNode Divide(ExitValueNode a, ExitValueNode b)
        {
            throw new InvalidOperationException("Cannot divide exits.");
        }

        public override ExitValueNode GetValue(int a)
        {
            throw new InvalidOperationException($"Integer {a} is not an exit.");
        }

        public override ExitValueNode GetValue(float a)
        {
            throw new InvalidOperationException($"Float {a} is not an exit.");
        }

        public override ExitValueNode GetValue(string a)
        {
            throw new InvalidOperationException($"String {a} is not an exit.");
        }

        public override ExitValueNode GetValue(ArrayNode a)
        {
            throw new InvalidOperationException($"Array is not an exit.");
        }

        public override ExitValueNode GetValue(ExitValueNode a) => a;
    }
}