using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes
{
    public abstract class ExpressionNode : Node<IExpressionVisitor>
    {
    }
}