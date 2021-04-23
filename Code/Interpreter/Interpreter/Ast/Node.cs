namespace Interpreter.Ast
{
    public abstract class Node<TVisitor>
    {
        public abstract void Accept(TVisitor visitor);
    }
}