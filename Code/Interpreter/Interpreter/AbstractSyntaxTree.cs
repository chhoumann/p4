using System;
using Antlr4.Runtime.Tree;

namespace Interpreter
{
    public sealed class AbstractSyntaxTree : IVisitor
    {
        public AbstractSyntaxTree(IParseTree parseTree)
        {
            Console.WriteLine(parseTree.GetChild(0));
        }
        
        public void Visit(GameObject gameObject)
        {
            
        }

        public void Visit(GameObjectType gameObjectType)
        {
        }

        public void Visit(GameObjectContents gameObjectContents)
        {
        }
    }
}