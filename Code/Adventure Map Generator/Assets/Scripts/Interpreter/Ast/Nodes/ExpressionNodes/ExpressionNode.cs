using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes
{
    internal abstract class ExpressionNode : Node<IExpressionVisitor>
    {
    }
}