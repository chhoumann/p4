using Antlr4.Runtime;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ArrayCalculator : Calculator<ArrayNode>
    {
        public ArrayCalculator(IToken token) : base(token) { }
        
        public override ArrayNode Add(ArrayNode a, ArrayNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot add arrays.", Token);
            
            return null;
        }

        public override ArrayNode Subtract(ArrayNode a, ArrayNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot subtract arrays.", Token);
            
            return null;
        }

        public override ArrayNode Multiply(ArrayNode a, ArrayNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot multiply arrays.", Token);
            
            return null;
        }

        public override ArrayNode Divide(ArrayNode a, ArrayNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot divide arrays.", Token);
            
            return null;
        }

        public override ArrayNode GetValue(int a)
        {
            DazelCompiler.Logger.EmitError($"Integer {a} is not an array.", Token);
            
            return null;
        }

        public override ArrayNode GetValue(float a)
        {
            DazelCompiler.Logger.EmitError($"Float {a} is not an array.", Token);
            
            return null;
        }

        public override ArrayNode GetValue(string a)
        {
            DazelCompiler.Logger.EmitError($"String {a} is not an array.", Token);
            
            return null;
        }

        public override ArrayNode GetValue(ArrayNode a) => a;

        public override ArrayNode GetValue(ExitValueNode a)
        {
            DazelCompiler.Logger.EmitError("ExitValue is not an array.", Token);
            
            return null;
        }
    }
}