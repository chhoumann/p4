using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class MovePattern : GameObjectType
    {
        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}