namespace Interpreter
{
    public sealed class GameObjectType : INode
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}