using Dazel.Compiler.Ast.Nodes.GameObjectNodes;

namespace Dazel.Compiler.Ast.Visitors
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