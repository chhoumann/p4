using Antlr4.Runtime;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ExitValueCalculator : Calculator<ExitValueNode>
    {
        public ExitValueCalculator(IToken token) : base(token) { }
        
        public override ExitValueNode Add(ExitValueNode a, ExitValueNode b)
        {
            DazelLogger.EmitError("Cannot add exits.", Token);
            
            return null;
        }

        public override ExitValueNode Subtract(ExitValueNode a, ExitValueNode b)
        {
            DazelLogger.EmitError("Cannot subtract exits.", Token);
            
            return null;
        }

        public override ExitValueNode Multiply(ExitValueNode a, ExitValueNode b)
        {
            DazelLogger.EmitError("Cannot multiply exits.", Token);
            
            return null;
        }

        public override ExitValueNode Divide(ExitValueNode a, ExitValueNode b)
        {
            DazelLogger.EmitError("Cannot divide exits.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(int a)
        {
            DazelLogger.EmitError($"Integer {a} is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(float a)
        {
            DazelLogger.EmitError($"Float {a} is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(string a)
        {
            DazelLogger.EmitError($"String {a} is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(ArrayNode a)
        {
            DazelLogger.EmitError($"Array is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(ExitValueNode a) => a;
    }
}