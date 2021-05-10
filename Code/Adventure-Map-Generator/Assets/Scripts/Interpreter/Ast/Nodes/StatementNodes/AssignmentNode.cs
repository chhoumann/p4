using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes;
using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes
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