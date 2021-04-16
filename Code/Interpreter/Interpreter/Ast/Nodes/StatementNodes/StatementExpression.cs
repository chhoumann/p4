namespace Interpreter.Ast.Nodes.StatementNodes
{
    public abstract class StatementExpression : StatementNode
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}