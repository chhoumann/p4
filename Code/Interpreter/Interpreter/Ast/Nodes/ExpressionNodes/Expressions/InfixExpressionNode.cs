namespace Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    internal abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }
}