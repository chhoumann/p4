namespace Interpreter
{
    public sealed class GameObject : Node
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GameObject(string text) : base(text)
        {
        }
    }
}