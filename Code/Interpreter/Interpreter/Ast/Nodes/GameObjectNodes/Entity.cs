using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class Entity : GameObjectType
    {
        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}