using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    internal sealed class Screen : GameObjectType
    {
        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}