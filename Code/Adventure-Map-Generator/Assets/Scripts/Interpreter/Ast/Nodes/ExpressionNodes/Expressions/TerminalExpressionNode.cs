using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class TerminalExpressionNode : ExpressionNode
    {
        public ExpressionNode Child { get; set; }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}