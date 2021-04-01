using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class GameObjectVisitor : IGameObjectVisitor
    {
        public GameObjectNode VisitGameObject(DazelParser.GameObjectContext context)
        {
            GameObjectType type;
            
            switch (context.gameObjectType.Type)
            {
                case DazelLexer.SCREEN:
                    type = new ScreenType();
                    break;
                case DazelLexer.ENTITY:
                    type = new EntityType();
                    break;
                case DazelLexer.MOVE_PATTERN:
                    type = new MovePatternType();
                    break;
                default:
                    throw new ArgumentException("Type is not a GameObjectType!");
            }

            GameObject gameObject = new()
            {
                Type = type,
                Contents = VisitGameObjectContents(context.gameObjectContents())
            };
            
            gameObject.Accept(this);

            return gameObject;
        }

        public List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            if (context.ChildCount == 1)
            {
                VisitGameObjectContent(context.gameObjectContent());
            }

            return new List<GameObjectContent>();
        }

        public GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {
            StatementNode statements = new StatementVisitor().VisitStatementList(context.statementList());

            switch (context.screenContentType.Type)
            {
                case DazelLexer.MAP:
                    break;
            }
            
            return new GameObjectContent();
        }

        public EntityType VisitEntityType(DazelParser.EntityTypeContext context)
        {
            return null;
        }

        public ScreenType VisitScreenType(DazelParser.ScreenTypeContext context)
        {
            return null;
        }

        public MovePatternType VisitMovePattern(DazelParser.MovePatternTypeContext context)
        {
            return null;
        }
    }
}