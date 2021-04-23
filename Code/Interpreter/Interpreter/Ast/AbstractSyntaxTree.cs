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
    }
}