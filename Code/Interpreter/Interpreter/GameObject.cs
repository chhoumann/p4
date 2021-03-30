namespace Interpreter
{
    public sealed class GameObject : INode
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}