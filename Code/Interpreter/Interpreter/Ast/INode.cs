namespace Interpreter.Ast
{
    public interface INode
    {
        void Accept(IVisitor visitor);
    }
}