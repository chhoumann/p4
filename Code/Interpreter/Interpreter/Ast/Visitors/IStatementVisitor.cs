﻿using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast.Visitors
{
    internal interface IStatementVisitor
    {
        void Visit(StatementBlock statementBlock);
        void Visit(IfStatement ifStatement);
        void Visit(RepeatNode repeatNode);
        void Visit(AssignmentNode assignmentNode);
        void Visit(FunctionInvocation functionInvocation);
    }
}