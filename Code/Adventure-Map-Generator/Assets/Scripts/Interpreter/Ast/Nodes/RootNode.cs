using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;

namespace P4.MapGenerator.Interpreter.Ast.Nodes
{
    public sealed class RootNode
    {
        public Dictionary<string, GameObjectNode> GameObjects { get; set; }
    }
}