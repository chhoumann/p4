namespace Interpreter
{
    public sealed class GameObjectContents : INode
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}