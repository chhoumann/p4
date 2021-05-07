namespace P4.MapGenerator.Interpreter.Ast.Nodes
{
    internal abstract class Node<TVisitor>
    {
        public abstract void Accept(TVisitor visitor);
    }
}