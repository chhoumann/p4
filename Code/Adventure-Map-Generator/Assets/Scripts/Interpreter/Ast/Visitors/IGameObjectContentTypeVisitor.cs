using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;

namespace P4.MapGenerator.Interpreter.Ast.Visitors
{
    public interface IGameObjectContentTypeVisitor
    {
        void Visit(MapTypeNode mapTypeNode);
        void Visit(OnScreenEnteredTypeNode onScreenEnteredTypeNode);
        void Visit(DataTypeNodeNode dataTypeNodeNode);
        void Visit(EntitiesTypeNodeNode entitiesTypeNodeNode);
        void Visit(ExitsTypeNodeNode exitsTypeNodeNode);
        void Visit(PatternTypeNode patternTypeNode);
    }
}