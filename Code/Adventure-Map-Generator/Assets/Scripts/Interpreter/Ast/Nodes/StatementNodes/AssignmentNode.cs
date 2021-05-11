using Dazel.Interpreter.Ast.Nodes.ExpressionNodes;
using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.StatementNodes
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