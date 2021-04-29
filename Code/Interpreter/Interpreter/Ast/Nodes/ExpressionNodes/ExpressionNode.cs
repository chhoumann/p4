using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    internal abstract class ExpressionNode : Node<IExpressionVisitor>
    {
    }
}