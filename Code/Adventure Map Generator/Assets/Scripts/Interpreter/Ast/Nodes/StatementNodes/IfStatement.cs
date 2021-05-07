﻿using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class IfStatement : StatementNode
    {
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}