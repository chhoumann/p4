using System.Collections.Generic;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    internal sealed class GameObject : GameObjectNode
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