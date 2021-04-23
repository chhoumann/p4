using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast.Nodes
{
    public sealed class RootNode
    {
        public Dictionary<string, GameObject> GameObjects { get; set; }
    }
}