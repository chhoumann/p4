using System.Collections.Generic;
using Interpreter.Ast.Nodes;

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
            return false;
        }
    }
}