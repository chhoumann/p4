using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public sealed class GameObjectVisitor : DazelBaseVisitor<GameObjectNode>, IGameObjectVisitor
    {
        public override GameObjectNode VisitGameObject(DazelParser.GameObjectContext context)
        {
            var gameObjectType = VisitGameObjectType(context.gameObjectType());
            var gameObjectContents = VisitGameObjectContents(context.gameObjectContents());

            return new GameObject();
        }

        public override GameObjectNode VisitGameObjectType(DazelParser.GameObjectTypeContext context)
        {
            return new GameObjectType();
        }

        public override GameObjectNode VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            if (context.ChildCount == 1)
            {
                VisitGameObjectContent(context.gameObjectContent());
            }

            return new GameObjectContents();
        }

        public override GameObjectNode VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {
            var contentType = VisitContentType(context.contentType());
            var statements = new StatementVisitor().VisitStatementList(context.statementList());

            return new GameObjectContent();
        }

        public override GameObjectNode VisitContentType(DazelParser.ContentTypeContext context)
        {
            return new GameObjectContentType();
        }
    }
}