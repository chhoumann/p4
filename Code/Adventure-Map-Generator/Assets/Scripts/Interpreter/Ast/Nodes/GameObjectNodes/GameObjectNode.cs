using System.Collections.Generic;
using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectNode : GameObjectNodeBase
    {
        public string Identifier { get; set; }
        public GameObjectTypeNode TypeNode { get; set; }
        public List<GameObjectContentNode> Contents { get; set; }

        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}