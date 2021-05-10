using P4.MapGenerator.Interpreter.Ast.Visitors;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class StringNode : ValueNode
    {
        public string Value { get; set; }

        public StringNode()
        {
            Type = SymbolType.String;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}