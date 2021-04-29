using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal abstract class ValueNode : ExpressionNode
    {
        public SymbolType Type { get; set; }
    }
}