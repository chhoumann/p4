using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal sealed class FloatValue : ValueNode
    {
        public float Value { get; set; }

        public FloatValue()
        {
            this.Type = SymbolType.Float;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}