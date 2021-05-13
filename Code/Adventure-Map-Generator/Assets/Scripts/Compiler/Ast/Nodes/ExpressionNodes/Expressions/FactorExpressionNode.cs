using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions
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