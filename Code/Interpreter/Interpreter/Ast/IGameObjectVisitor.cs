using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public interface IGameObjectVisitor
    {
        GameObjectNode VisitGameObject(DazelParser.GameObjectContext context);
        GameObjectType VisitGameObjectType(DazelParser.GameObjectTypeContext context);
        GameObjectContents VisitGameObjectContents(DazelParser.GameObjectContentsContext context);
        GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context);
        GameObjectEntityType VisitEntityType(DazelParser.ContentEntityTypeContext context);
        GameObjectScreenType VisitScreenType(DazelParser.ContentScreenTypeContext context);
        GameObjectContentMovePattern VisitContentMovePattern(DazelParser.ContentMovePatternTypeContext context);
    }
}