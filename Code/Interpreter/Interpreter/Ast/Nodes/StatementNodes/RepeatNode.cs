using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    internal sealed class RepeatNode : StatementNode
    {
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}