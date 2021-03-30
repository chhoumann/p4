namespace Interpreter
{
    public interface INode
    {
        void Accept(IVisitor visitor);
    }
}