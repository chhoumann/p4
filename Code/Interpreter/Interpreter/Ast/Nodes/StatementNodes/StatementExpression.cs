using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class StatementExpression : StatementNode
    {
        public ExpressionNode Expression { get; set; }
    }
}