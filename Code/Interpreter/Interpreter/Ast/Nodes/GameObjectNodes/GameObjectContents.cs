namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectContents : GameObjectNode
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}