using System.Collections.Generic;
using Interpreter.Ast.Nodes;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public RootNode Root { get; }
        
        public AbstractSyntaxTree(RootNode root)
        {
            Root = root;
        }

        public bool TryRetrieveNode<TNode>(List<string> identifierList, out TNode node)
        {
            GameObject start = Root.GameObjects[identifierList[0]];

            foreach (GameObjectContent gameObjectContent in start.Contents)
            {
                if (nameof(gameObjectContent.Type) == identifierList[1])
                {
                    
                }
            }
            
            return false;
        }
    }
}