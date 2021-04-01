using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class GameObjectVisitor : DazelBaseVisitor<GameObjectNode>, IGameObjectVisitor
    {
        public override GameObjectNode VisitGameObject(DazelParser.GameObjectContext context)
        {
            VisitGameObjectType(context.gameObjectType());
            

            return new GameObject();
        }

        public override GameObjectNode VisitGameObjectType(DazelParser.GameObjectTypeContext context)
        {
            VisitGameObjectContents(context.gameObjectContents());
            return new GameObjectType(context);
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
            GameObjectNode contentScreenType = VisitContentScreenType(context.contentScreenType());
            GameObjectNode contentEntityType = VisitContentEntityType(context.contentEntityType());
            GameObjectNode contentMovePattern = VisitContentMovePatternType(context.contentMovePatternType());
            StatementNode statements = new StatementVisitor().VisitStatementList(context.statementList());

            return new GameObjectContent();
        }

        public override GameObjectNode VisitContentScreenType(DazelParser.ContentScreenTypeContext context)
        {
            return new GameObjectScreenType();
        }

        public override GameObjectNode VisitContentEntityType(DazelParser.ContentEntityTypeContext context)
        {
            return new GameObjectEntityType();
        }

        public override GameObjectNode VisitContentMovePatternType(DazelParser.ContentMovePatternTypeContext context)
        {
            return new GameObjectContentMovePattern();
        }
        
    }
}