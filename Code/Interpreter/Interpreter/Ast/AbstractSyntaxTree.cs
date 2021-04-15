using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public GameObject Root { get; }
        
        public AbstractSyntaxTree(GameObject root)
        {
            Root = root;
        }
    }
}