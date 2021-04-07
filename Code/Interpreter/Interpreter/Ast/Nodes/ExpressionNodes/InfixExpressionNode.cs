﻿namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }
}