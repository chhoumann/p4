using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class FactorExpressionNode : InfixExpressionNode
    {
        public FactorOperationNode OperationNode { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}