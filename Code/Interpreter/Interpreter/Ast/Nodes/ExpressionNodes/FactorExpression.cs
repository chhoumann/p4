﻿namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class FactorExpression : InfixExpressionNode
    {
        public FactorOperation Operation { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            Left.PrintMe();
            Operation.PrintMe();
            Right.PrintMe();
        }
    }
}