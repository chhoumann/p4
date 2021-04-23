﻿using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class SumOperation : OperationNode
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}