using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes
{
    public abstract class ExpressionNode : Node<IExpressionVisitor>
    {
    }
}