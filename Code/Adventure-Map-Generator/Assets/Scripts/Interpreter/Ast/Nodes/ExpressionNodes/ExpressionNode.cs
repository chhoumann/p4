using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes
{
    public abstract class ExpressionNode : Node<IExpressionVisitor>
    {
    }
}