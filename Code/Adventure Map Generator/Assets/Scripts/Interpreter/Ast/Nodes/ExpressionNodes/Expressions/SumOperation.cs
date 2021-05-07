﻿using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class SumOperation : OperationNode
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}