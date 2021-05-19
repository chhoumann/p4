using System;
using Antlr4.Runtime;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class NoOpCalculator<T> : Calculator<T>
    {
        public NoOpCalculator(IToken token) : base(token) { }
        
        public override T Add(T a, T b)
        {
            throw new NotImplementedException();
        }

        public override T Subtract(T a, T b)
        {
            throw new NotImplementedException();
        }

        public override T Multiply(T a, T b)
        {
            throw new NotImplementedException();
        }

        public override T Divide(T a, T b)
        {
            throw new NotImplementedException();
        }

        public override T GetValue(int a)
        {
            throw new NotImplementedException();
        }

        public override T GetValue(float a)
        {
            throw new NotImplementedException();
        }

        public override T GetValue(string a)
        {
            throw new NotImplementedException();
        }

        public override T GetValue(ArrayNode a)
        {
            throw new NotImplementedException();
        }

        public override T GetValue(ExitValueNode a)
        {
            throw new NotImplementedException();
        }
    }
}