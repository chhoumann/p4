using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class IfStatement : StatementNode
    {
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}