﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class ArrayNode : ValueNode
    {
        public List<ValueNode> Values { get; set; }

        public ArrayNode()
        {
            this.Type = SymbolType.Array;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Vector2 ToVector2()
        {
            if (Values.Count == 2)
            {
                IntValueNode x = Values[0] as IntValueNode;
                IntValueNode y = Values[1] as IntValueNode;

                return new Vector2(x.Value, y.Value);
            }

            throw new ArgumentException($"Invalid number of arguments: {Values.Count}.");
        }
    }
}