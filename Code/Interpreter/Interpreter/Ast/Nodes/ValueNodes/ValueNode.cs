using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ValueNodes
{
    public abstract class ValueNode : Node<IValueVisitor>
    {
    }
}