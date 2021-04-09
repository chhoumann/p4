namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class IfStatement : StatementNode
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}