using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class EntityNode : GameObjectTypeNode
    {
        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}