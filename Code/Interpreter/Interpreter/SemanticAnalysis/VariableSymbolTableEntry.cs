using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.SemanticAnalysis
{
    public class VariableSymbolTableEntry : SymbolTableEntry
    {
        private readonly ExpressionNode expression;
        
        public VariableSymbolTableEntry(string identifier, SymbolType type, ExpressionNode expression) : base(identifier, type)
        {
            this.expression = expression;
        }
    }
}