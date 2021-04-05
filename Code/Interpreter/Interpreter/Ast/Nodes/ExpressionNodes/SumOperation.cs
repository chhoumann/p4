﻿using System;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class SumOperation : OperationNode
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            Console.Write($" {Operation} ");
        }
    }
}