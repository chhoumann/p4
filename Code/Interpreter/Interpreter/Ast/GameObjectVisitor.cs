using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes;

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
                Type = type,
                Contents = VisitGameObjectContents(context.gameObjectContents())
            };
            
            gameObject.Accept(this);

            return gameObject;
        }

        public List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            List<GameObjectContent> contents = new();

            for (int i = 0; i < context.children.Count; i++)
            {
                contents.Add(VisitGameObjectContent(context.gameObjectContent()));
            }

            return contents;
        }

        public GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {
            GameObjectContent content = new();
            /*GameObjectContent content = new()
            {
                Statements = new StatementVisitor().VisitStatementList(context.statementList());
            };*/

            return content;
        }
    }
}