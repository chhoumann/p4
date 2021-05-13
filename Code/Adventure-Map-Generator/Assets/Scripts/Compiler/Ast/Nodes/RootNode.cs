using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;

namespace Dazel.Compiler.Ast.Nodes
{
    public sealed class RootNode
    {
        public Dictionary<string, GameObjectNode> GameObjects { get; set; }
    }
}