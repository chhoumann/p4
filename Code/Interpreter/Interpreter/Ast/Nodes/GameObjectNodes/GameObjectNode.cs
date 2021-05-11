using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    internal abstract class GameObjectNode : Node<IGameObjectVisitor>
    {
    }
}