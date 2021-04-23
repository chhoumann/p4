using System.Collections.Generic;
using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class ArrayNode : ValueNode
    {
        public List<ValueNode> Values { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}