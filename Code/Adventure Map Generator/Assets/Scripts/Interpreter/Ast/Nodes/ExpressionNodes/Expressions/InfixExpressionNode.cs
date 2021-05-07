namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    public abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }
}