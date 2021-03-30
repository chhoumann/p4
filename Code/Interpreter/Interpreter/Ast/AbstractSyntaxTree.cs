using System;
using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public abstract class Node
    {
        
    }
    
    public sealed class AbstractSyntaxTree : IVisitor
    {
        public AbstractSyntaxTree(IParseTree parseTree)
        {
            IParseTree gameObjectParseTreeNode = parseTree.GetChild(0);

            new GameObject().Accept(this);
        }
        
        public void Visit(GameObject gameObject)
        { 
            Console.WriteLine(gameObject.Type);
            Console.WriteLine(gameObject.Identifier);
            
            for (int i = 0; i < gameObject.Contents.ChildCount; i++)
            {
                Console.WriteLine(gameObject.Contents.GetChild(i).GetText());
            }
        }

        public void Visit(GameObjectType gameObjectType)
        {
        }

        public void Visit(GameObjectContents gameObjectContents)
        {
        }
    }
}