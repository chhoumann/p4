using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.StatementNodes
{
    public sealed class RepeatNode : StatementNode
    {
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}