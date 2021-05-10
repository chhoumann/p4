using System.Collections.Generic;
using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class MemberAccessNode : ValueNode
    {
        public readonly List<string> Identifiers = new List<string>();

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}