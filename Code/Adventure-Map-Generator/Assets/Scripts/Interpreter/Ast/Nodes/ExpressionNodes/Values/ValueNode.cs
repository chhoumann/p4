using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public abstract class ValueNode : ExpressionNode
    {
        public SymbolType Type { get; set; }
    }
}