using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public interface IGameObjectVisitor
    {
        GameObject VisitGameObject(DazelParser.GameObjectContext context);
        List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context);
        GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context);
    }
}