using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class VariableSymbolTableEntry : SymbolTableEntry
    {
        public ValueNode ValueNode { get; private set; }
        
        public VariableSymbolTableEntry(ValueNode valueNode, SymbolType type) : base(type)
        {
            ValueNode = valueNode;
        }
    }
}