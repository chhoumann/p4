﻿namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class SumExpression : InfixExpressionNode
    {
        public SumOperation Operation { get; set; }
    }
}