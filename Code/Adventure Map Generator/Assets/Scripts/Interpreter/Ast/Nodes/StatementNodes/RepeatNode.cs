﻿using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes
{
    internal sealed class RepeatNode : StatementNode
    {
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}