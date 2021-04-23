using System.Collections.Generic;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ValueNodes
{
    public sealed class MemberAccess : ValueNode
    {
        public readonly List<string> Identifiers = new();

        public override void Accept(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}