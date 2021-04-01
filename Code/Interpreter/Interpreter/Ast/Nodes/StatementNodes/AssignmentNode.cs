using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class AssignmentNode : StatementNode
    {
        public string Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
    }
}