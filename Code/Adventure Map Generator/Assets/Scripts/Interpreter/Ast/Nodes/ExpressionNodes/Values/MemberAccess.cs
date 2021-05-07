using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal sealed class MemberAccess : ValueNode
    {
        public readonly List<string> Identifiers = new List<string>();

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}