namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public abstract class OperationNode : ExpressionNode
    {
        public char Operation { get; set; }
    }
}