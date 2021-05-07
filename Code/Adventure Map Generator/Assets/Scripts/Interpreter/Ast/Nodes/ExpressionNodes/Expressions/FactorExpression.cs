using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class FactorExpression : InfixExpressionNode
    {
        public FactorOperation Operation { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}