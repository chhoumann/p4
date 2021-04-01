using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public interface IGameObjectVisitor
    {
        GameObjectNode VisitGameObject(DazelParser.GameObjectContext context);
        GameObjectType VisitGameObjectType(DazelParser.GameObjectTypeContext context);
        List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context);
        GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context);
        EntityType VisitEntityType(DazelParser.EntityTypeContext context);
        ScreenType VisitScreenType(DazelParser.ScreenTypeContext context);
        MovePatternType VisitMovePattern(DazelParser.MovePatternTypeContext context);
    }
}