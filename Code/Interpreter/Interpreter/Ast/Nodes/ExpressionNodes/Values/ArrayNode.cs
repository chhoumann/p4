using System;
using System.Collections.Generic;
using System.Numerics;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal sealed class ArrayNode : ValueNode
    {
        public List<ValueNode> Values { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Vector2 ToVector2()
        {
            if (Values.Count == 2)
            {
                IntValue x = Values[0] as IntValue;
                IntValue y = Values[1] as IntValue;

                return new Vector2(x.Value, y.Value);
            }

            throw new ArgumentException($"Invalid number of arguments: {Values.Count}.");
        }
    }
}