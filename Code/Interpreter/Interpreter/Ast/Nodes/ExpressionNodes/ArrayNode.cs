using System;
using System.Collections.Generic;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class ArrayNode : Value
    {
        public List<Value> Values { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}