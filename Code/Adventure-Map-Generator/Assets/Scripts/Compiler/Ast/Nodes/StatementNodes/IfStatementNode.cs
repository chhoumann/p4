using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.StatementNodes
{
    public sealed class IfStatementNode : StatementNode
    {
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}