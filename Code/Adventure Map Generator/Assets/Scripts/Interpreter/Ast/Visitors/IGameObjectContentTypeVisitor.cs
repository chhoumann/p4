using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;

namespace P4.MapGenerator.Interpreter.Ast.Visitors
{
    internal interface IGameObjectContentTypeVisitor
    {
        void Visit(MapType mapType);
        void Visit(OnScreenEnteredType onScreenEnteredType);
        void Visit(DataType dataType);
        void Visit(EntitiesType entitiesType);
        void Visit(ExitsType exitsType);
        void Visit(PatternType patternType);
    }
}