using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    internal sealed class TerminalExpression : ExpressionNode
    {
        public ExpressionNode Child { get; set; }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}