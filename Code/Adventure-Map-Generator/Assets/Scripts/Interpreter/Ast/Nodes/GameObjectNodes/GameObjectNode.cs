using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes
{
    public abstract class GameObjectNode : Node<IGameObjectVisitor>
    {
    }
}