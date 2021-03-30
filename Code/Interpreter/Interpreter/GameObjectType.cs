namespace Interpreter
{
    public sealed class GameObjectType : Node
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GameObjectType(string text) : base(text)
        {
        }
    }
}