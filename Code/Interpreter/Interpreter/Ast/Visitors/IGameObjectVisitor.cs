using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;

namespace Interpreter.Ast.Visitors
{
    public interface IGameObjectVisitor
    {
        void Visit(GameObject gameObject);
        void Visit(GameObjectContent gameObjectContent);
        void Visit(MovePattern movePattern);
        
        void Visit(Entity entity);
        void Visit(Screen screen);
    }
}