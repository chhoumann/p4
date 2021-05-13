using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public abstract class ValueNode : ExpressionNode
    {
        public SymbolType Type { get; set; }
    }
}