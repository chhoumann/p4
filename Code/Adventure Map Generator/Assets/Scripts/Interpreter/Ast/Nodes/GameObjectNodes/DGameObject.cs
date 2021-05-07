using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes
{
    internal sealed class DGameObject : GameObjectNode
    {
        public string Identifier { get; set; }
        public GameObjectType Type { get; set; }
        public List<GameObjectContent> Contents { get; set; }

        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}