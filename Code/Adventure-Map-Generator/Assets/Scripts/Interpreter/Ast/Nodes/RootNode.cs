using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;

namespace Dazel.Interpreter.Ast.Nodes
{
    public sealed class RootNode
    {
        public Dictionary<string, GameObjectNode> GameObjects { get; set; }
    }
}