using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class GameObjectVisitor : IGameObjectVisitor
    {
        public GameObjectNode VisitGameObject(DazelParser.GameObjectContext context)
        {
            
            var gameObject = new GameObject()
            {
                Type = (GameObjectType)VisitGameObjectType(context.gameObjectType())
            };

            return new GameObject();
        }

        public GameObjectNode VisitGameObjectType(DazelParser.GameObjectTypeContext context)
        {
            VisitGameObjectContents(context.gameObjectContents());
            return new GameObjectType(context);
        }

        public GameObjectNode VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            if (context.ChildCount == 1)
            {
                VisitGameObjectContent(context.gameObjectContent());
            }

            return new GameObjectContents();
        }

        public GameObjectNode VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {
            GameObjectNode contentScreenType = VisitContentScreenType(context.contentScreenType());
            GameObjectNode contentEntityType = VisitContentEntityType(context.contentEntityType());
            GameObjectNode contentMovePattern = VisitContentMovePatternType(context.contentMovePatternType());
            StatementNode statements = new StatementVisitor().VisitStatementList(context.statementList());

            return new GameObjectContent();
        }

        public GameObjectNode VisitContentScreenType(DazelParser.ContentScreenTypeContext context)
        {
            return new GameObjectScreenType();
        }

        public GameObjectNode VisitContentEntityType(DazelParser.ContentEntityTypeContext context)
        {
            return new GameObjectEntityType();
        }

        public GameObjectNode VisitContentMovePatternType(DazelParser.ContentMovePatternTypeContext context)
        {
            return new GameObjectContentMovePattern();
        }
        
    }
}