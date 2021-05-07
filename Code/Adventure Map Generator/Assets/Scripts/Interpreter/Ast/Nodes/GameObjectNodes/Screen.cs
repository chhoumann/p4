using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class Screen : GameObjectType
    {
        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}