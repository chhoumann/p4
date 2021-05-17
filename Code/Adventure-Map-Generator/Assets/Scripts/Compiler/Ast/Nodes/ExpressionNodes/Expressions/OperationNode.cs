namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions
{
    public abstract class OperationNode : ExpressionNode
    {
        public char Operator { get; set; }
    }
}