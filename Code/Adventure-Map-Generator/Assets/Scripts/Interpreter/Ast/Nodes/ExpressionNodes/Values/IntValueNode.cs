using P4.MapGenerator.Interpreter.Ast.Visitors;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class IntValueNode : ValueNode
    {
        public int Value { get; set; }

        public IntValueNode()
        {
            Type = SymbolType.Integer;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}