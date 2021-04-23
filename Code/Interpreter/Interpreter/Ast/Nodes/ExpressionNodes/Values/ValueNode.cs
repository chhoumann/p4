using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public abstract class ValueNode : ExpressionNode
    {
        public SymbolType Type { get; set; }
    }
}