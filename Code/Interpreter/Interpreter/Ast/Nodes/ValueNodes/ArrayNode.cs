using System.Collections.Generic;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ValueNodes
{
    public sealed class ArrayNode : ValueNode
    {
        public List<ValueNode> Values { get; set; }

        public override void Accept(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}