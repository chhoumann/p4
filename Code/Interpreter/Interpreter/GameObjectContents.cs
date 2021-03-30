namespace Interpreter
{
    public sealed class GameObjectContents : Node
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GameObjectContents(string text) : base(text)
        {
        }
    }
}