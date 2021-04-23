using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ValueNodes
{
    public sealed class IdentifierValue : ValueNode
    {
        public string Value { get; set; }

        public override void Accept(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}