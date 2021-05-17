using Antlr4.Runtime;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ExitValueCalculator : Calculator<ExitValueNode>
    {
        public ExitValueCalculator(IToken token) : base(token) { }
        
        public override ExitValueNode Add(ExitValueNode a, ExitValueNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot add exits.", Token);
            
            return null;
        }

        public override ExitValueNode Subtract(ExitValueNode a, ExitValueNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot subtract exits.", Token);
            
            return null;
        }

        public override ExitValueNode Multiply(ExitValueNode a, ExitValueNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot multiply exits.", Token);
            
            return null;
        }

        public override ExitValueNode Divide(ExitValueNode a, ExitValueNode b)
        {
            DazelCompiler.Logger.EmitError("Cannot divide exits.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(int a)
        {
            DazelCompiler.Logger.EmitError($"Integer {a} is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(float a)
        {
            DazelCompiler.Logger.EmitError($"Float {a} is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(string a)
        {
            DazelCompiler.Logger.EmitError($"String {a} is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(ArrayNode a)
        {
            DazelCompiler.Logger.EmitError($"Array is not an exit.", Token);
            
            return null;
        }

        public override ExitValueNode GetValue(ExitValueNode a) => a;
    }
}