namespace Dazel.Interpreter.Ast.Nodes
{
    public abstract class Node<TVisitor>
    {
        public abstract void Accept(TVisitor visitor);
    }
}