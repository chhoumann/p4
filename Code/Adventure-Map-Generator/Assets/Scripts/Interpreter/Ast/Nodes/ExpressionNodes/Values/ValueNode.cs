using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public abstract class ValueNode : ExpressionNode
    {
        public SymbolType Type { get; set; }
    }
}