using Dazel.Compiler.Ast.Nodes.ExpressionNodes;
using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.StatementNodes
{
    public sealed class AssignmentNode : StatementExpressionNode
    {
        public string Identifier { get; set; }
        public ExpressionNode Expression { get; set; }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}