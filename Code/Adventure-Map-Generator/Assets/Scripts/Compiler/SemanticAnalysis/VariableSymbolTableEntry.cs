using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class VariableSymbolTableEntry : SymbolTableEntry
    {
        public ValueNode ValueNode { get; }
        
        public VariableSymbolTableEntry(ValueNode valueNode, SymbolType type) : base(type)
        {
            ValueNode = valueNode;
        }

        public override string ToString()
        {
            return ValueNode.ToString();
        }
    }
}