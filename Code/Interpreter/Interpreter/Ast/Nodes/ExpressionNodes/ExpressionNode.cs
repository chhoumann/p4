using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public abstract class ExpressionNode : Node<IExpressionVisitor>
    {
    }
}