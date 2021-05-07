using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;

namespace P4.MapGenerator.Interpreter.Ast.Visitors
{
    public interface IGameObjectVisitor
    {
        void Visit(DGameObject gameObject);
        void Visit(GameObjectContent gameObjectContent);
        void Visit(MovePattern movePattern);
        
        void Visit(Entity entity);
        void Visit(Screen screen);
    }
}