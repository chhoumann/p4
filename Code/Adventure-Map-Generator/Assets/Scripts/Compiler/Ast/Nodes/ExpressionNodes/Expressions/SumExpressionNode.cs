using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class SumExpressionNode : InfixExpressionNode
    {
        public SumOperationNode OperationNode { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}