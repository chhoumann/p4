using Dazel.Compiler.Ast.Nodes;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;

namespace Dazel.Compiler.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public AbstractSyntaxTree(RootNode root)
        {
            Root = root;
        }

        public RootNode Root { get; }

        public bool TryRetrieveGameObject(string identifier, out GameObjectNode gameObjectNode)
        {
            return Root.GameObjects.TryGetValue(identifier, out gameObjectNode);
        }
        
        public bool TryRetrieveGameObject(string identifier)
        {
            return Root.GameObjects.TryGetValue(identifier, out _);
        }
    }
}