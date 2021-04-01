using System;
using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class AbstractSyntaxTree 
    {
        public AbstractSyntaxTree(IParseTree parseTree)
        {
            GameObject gameObject = new GameObjectVisitor().VisitGameObject(parseTree.GetChild(0) as DazelParser.GameObjectContext);

            Console.WriteLine(gameObject.Type.ToString());
            foreach (GameObjectContent vContent in gameObject.Contents)
            {
                Console.WriteLine(vContent.GetType());
                foreach (StatementNode vStatement in vContent.Statements)
                {
                    Console.WriteLine(vStatement.GetType());
                }
            }
        }
    }
}