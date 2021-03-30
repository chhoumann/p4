namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public abstract class ExpressionNode
    {
        public virtual void Accept(IExpressionVisitor visitor)
        {
            
        }
    }
}