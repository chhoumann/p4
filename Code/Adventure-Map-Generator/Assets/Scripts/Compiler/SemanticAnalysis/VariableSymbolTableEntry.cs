using Dazel.Compiler.Ast.Nodes.ExpressionNodes;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class VariableSymbolTableEntry : SymbolTableEntry
    {
        public ExpressionNode ExpressionNode { get; set; }
        
        public VariableSymbolTableEntry(SymbolType type) : base(type)
        {
        }
    }
}