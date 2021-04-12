﻿namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class SumExpression : InfixExpressionNode
    {
        public SumOperation Operation { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}