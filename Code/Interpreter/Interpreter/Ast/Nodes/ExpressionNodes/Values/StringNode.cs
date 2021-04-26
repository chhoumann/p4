using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public class StringNode : ValueNode
    {
        public string Value { get; set; }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}