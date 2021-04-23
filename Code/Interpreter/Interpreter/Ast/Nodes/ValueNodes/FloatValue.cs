using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ValueNodes
{
    public sealed class FloatValue : ValueNode
    {
        public float Value { get; set; }

        public override void Accept(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}