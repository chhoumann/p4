using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;

namespace P4.MapGenerator.Interpreter.Ast.Visitors
{
    public interface IGameObjectVisitor
    {
        void Visit(GameObjectNode gameObjectNode);
        void Visit(GameObjectContentNode gameObjectContentNode);
        void Visit(MovePatternNode movePatternNode);
        
        void Visit(EntityNode entityNode);
        void Visit(ScreenNode screenNode);
    }
}