using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public interface IGameObjectVisitor
    {
        GameObjectNode VisitGameObject(DazelParser.GameObjectContext context);
        GameObjectNode VisitGameObjectType(DazelParser.GameObjectTypeContext context);
        GameObjectNode VisitGameObjectContents(DazelParser.GameObjectContentsContext context);
        GameObjectNode VisitGameObjectContent(DazelParser.GameObjectContentContext context);
        GameObjectNode VisitContentScreenType(DazelParser.ContentScreenTypeContext context);
        GameObjectNode VisitContentEntityType(DazelParser.ContentEntityTypeContext context);
        GameObjectNode VisitContentMovePatternType(DazelParser.ContentMovePatternTypeContext context);
    }
}