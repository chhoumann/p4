using System.Collections.Generic;
using System.Data;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObject : GameObjectNode
    {
        public GameObjectType Type { get; set; }
        public List<GameObjectContent> Contents { get; set; }
        
        public override void Accept(IGameObjectVisitor visitor)
        {
            
        }
    }
}