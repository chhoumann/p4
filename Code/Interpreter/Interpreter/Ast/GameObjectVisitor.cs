using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class GameObjectVisitor : IGameObjectVisitor
    {
        public GameObjectNode VisitGameObject(DazelParser.GameObjectContext context)
        {
            GameObject gameObject = new GameObject
            {
                Type = VisitGameObjectType(context.gameObjectType()),
                Contents = VisitGameObjectContents(context.gameObjectContents())
            };
            
            gameObject.Accept(this);

            return gameObject;
        }

        public GameObjectType VisitGameObjectType(DazelParser.GameObjectTypeContext context)
        {
            GameObjectType type;
            
            
            if (context.GetType() == typeof(DazelParser.ScreenContentTypeContext))
            {
                type = new ScreenType();
            }
            else if (context.GetType() == typeof(DazelParser.EntityContentTypeContext))
            {
                type = new EntityType();
            }
            else if (context.GetType() == typeof(DazelParser.MovePatternContentTypeContext))
            {
                type = new MovePatternType();
            }
            else
            {
                throw new ArgumentException("Type is not a GameObjectType!");
            }
                
            type.Accept(this);
            
            return type;
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