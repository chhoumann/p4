using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;

namespace Interpreter.Ast.Visitors
{
    public interface IGameObjectContentTypeVisitor
    {
        void Visit(MapType mapType);
        void Visit(OnScreenEnteredType onScreenEnteredType);
        void Visit(DataType dataType);
        void Visit(EntitiesType entitiesType);
        void Visit(ExitsType exitsType);
        void Visit(PatternType patternType);
    }
}