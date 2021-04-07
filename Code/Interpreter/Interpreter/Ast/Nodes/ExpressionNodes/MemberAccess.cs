using System;
using System.Collections.Generic;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class MemberAccess : Value
    {
        public readonly List<string> Identifiers = new();

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}