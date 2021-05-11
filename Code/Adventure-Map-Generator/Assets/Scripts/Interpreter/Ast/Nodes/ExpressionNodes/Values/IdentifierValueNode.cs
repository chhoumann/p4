using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class IdentifierValueNode : ValueNode
    {
        public string Value { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}