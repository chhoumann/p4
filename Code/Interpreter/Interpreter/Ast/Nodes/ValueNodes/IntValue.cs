using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ValueNodes
{
    public sealed class IntValue : ValueNode
    {
        public int Value { get; set; }
        public override void Accept(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}