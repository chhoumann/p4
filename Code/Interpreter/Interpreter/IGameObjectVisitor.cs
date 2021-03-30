using System.Data;

namespace Interpreter
{
    public interface IGameObjectVisitor
    {
        void Visit(GameObject go);
        void Visit(GameObjectType go_Type);
        void Visit(GameObjectContent go_Content);

    }
    
    public interface IGameObject 
}

