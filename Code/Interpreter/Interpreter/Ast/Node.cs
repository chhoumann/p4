namespace Interpreter.Ast
{
    public abstract class Node
    {
        public abstract void Accept(IVisitor visitor);
    }
}