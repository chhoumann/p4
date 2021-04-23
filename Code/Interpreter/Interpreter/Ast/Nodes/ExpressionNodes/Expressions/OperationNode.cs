namespace Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    public abstract class OperationNode : ExpressionNode
    {
        public char Operation { get; set; }
    }
}