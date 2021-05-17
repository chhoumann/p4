using Antlr4.Runtime;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public abstract class Calculator<T>
    {
        protected IToken Token { get; }
        
        public Calculator(IToken token)
        {
            Token = token;
        }
        
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
}