﻿namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class FactorExpression : InfixExpressionNode
    {
        public FactorOperation Operation { get; set; }
    }
}