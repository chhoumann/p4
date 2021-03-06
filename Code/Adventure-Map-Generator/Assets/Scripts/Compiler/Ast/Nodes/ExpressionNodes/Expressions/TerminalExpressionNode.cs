﻿using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions
{
    public sealed class TerminalExpressionNode : ExpressionNode
    {
        public ExpressionNode Child { get; set; }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}