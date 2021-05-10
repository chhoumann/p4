using P4.MapGenerator.Interpreter.Ast.Visitors;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class FloatValueNode : ValueNode
    {
        public float Value { get; set; }

        public FloatValueNode()
        {
            Type = SymbolType.Float;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}