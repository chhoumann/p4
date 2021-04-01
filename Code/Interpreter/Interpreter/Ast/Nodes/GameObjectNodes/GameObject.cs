using System.Data;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObject : GameObjectNode
    {
        public GameObjectType Type { get; set; }
        
        public override void Accept(IGameObjectVisitor visitor)
        {
            
        }
    }
}