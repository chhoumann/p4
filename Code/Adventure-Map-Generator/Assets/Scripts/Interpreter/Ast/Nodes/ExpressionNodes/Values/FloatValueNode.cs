using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
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