namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class RepeatNode : StatementNode
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            throw new System.NotImplementedException();
        }
    }
}