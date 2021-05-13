using System.Collections.Generic;
using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
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