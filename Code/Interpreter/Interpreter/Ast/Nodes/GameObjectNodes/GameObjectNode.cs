using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public abstract class GameObjectNode : Node<IGameObjectVisitor>
    {
    }
}