using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class SumOperationNode : OperationNode
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}