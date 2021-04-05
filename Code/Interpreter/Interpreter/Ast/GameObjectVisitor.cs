using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;

namespace Interpreter.Ast
{
    public sealed class GameObjectVisitor : IGameObjectVisitor
    {
        public GameObject VisitGameObject(DazelParser.GameObjectContext context)
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
                Identifier = context.GetChild(1).GetText(),
                Type = type,
                Contents = VisitGameObjectContents(context.gameObjectBlock().gameObjectContents())
            };
            
            return gameObject;
        }

        public List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            List<GameObjectContent> contents = new();

            if (context.gameObjectContent() == null) return contents;

            contents.Add(VisitGameObjectContent(context.gameObjectContent()));
            
            if (context.gameObjectContents() != null)
            {
                contents.AddRange(VisitGameObjectContents(context.gameObjectContents()));
            }

            return contents;
        }

        public GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {
            GameObjectContentType gameObjectContentType;
            
            switch (context.gameObjectContentType.Type)
            {
                case DazelLexer.MAP:
                    gameObjectContentType = new MapType();
                    break;
                case DazelLexer.ONSCREENENTERED:
                    gameObjectContentType = new OnScreenEnteredType();
                    break;
                case DazelLexer.ENTITIES:
                    gameObjectContentType = new EntitiesType();
                    break;
                case DazelLexer.EXITS:
                    gameObjectContentType = new ExitsType();
                    break;
                case DazelLexer.DATA:
                    gameObjectContentType = new DataType();
                    break;
                case DazelLexer.PATTERN:
                    gameObjectContentType = new PatternType();
                    break;
                default:
                    throw new ArgumentException("Invalid content type.");
            }
            
            GameObjectContent content = new()
            {
                Statements = new StatementVisitor().VisitStatementBlock(context.statementBlock()),
                Type = gameObjectContentType,
            };

            return content;
        }
    }
}