using System;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public abstract class Calculator<T>
    {
        public abstract T Add(T a, T b);
        public abstract T Subtract(T a, T b);
        public abstract T Multiply(T a, T b);
        public abstract T Divide(T a, T b);
        public abstract T GetValue(int a);
        public abstract T GetValue(float a);
        public abstract T GetValue(string a);
        public abstract T GetValue(ArrayNode a);
        public abstract T GetValue(ExitValueNode a);
    }

    public sealed class IdentifierCalculator : Calculator<ValueNode>
    {
        public override ValueNode Add(ValueNode a, ValueNode b)
        {
            throw new NotImplementedException();
        }

        public override ValueNode Subtract(ValueNode a, ValueNode b)
        {
            throw new NotImplementedException();
        }

        public override ValueNode Multiply(ValueNode a, ValueNode b)
        {
            throw new NotImplementedException();
        }

        public override ValueNode Divide(ValueNode a, ValueNode b)
        {
            throw new NotImplementedException();
        }

        public override ValueNode GetValue(int a)
        {
            throw new NotImplementedException();
        }

        public override ValueNode GetValue(float a)
        {
            throw new NotImplementedException();
        }

        public override ValueNode GetValue(string a)
        {
            throw new NotImplementedException();
        }

        public override ValueNode GetValue(ArrayNode a)
        {
            throw new NotImplementedException();
        }

        public override ValueNode GetValue(ExitValueNode a)
        {
            throw new NotImplementedException();
        }
    }
}