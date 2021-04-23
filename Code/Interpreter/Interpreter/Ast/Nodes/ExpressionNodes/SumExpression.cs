using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class SumExpression : InfixExpressionNode
    {
        public SumOperation Operation { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}