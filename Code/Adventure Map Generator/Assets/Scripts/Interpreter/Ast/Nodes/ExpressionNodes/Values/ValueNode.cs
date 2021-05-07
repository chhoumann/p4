using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal abstract class ValueNode : ExpressionNode
    {
        public SymbolType Type { get; set; }
    }
}