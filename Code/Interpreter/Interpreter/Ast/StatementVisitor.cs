using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class StatementVisitor : IStatementVisitor
    {
        public List<StatementNode> VisitStatementList(DazelParser.StatementListContext context)
        {
            List<StatementNode> statements = new();

            statements.Add(VisitStatement(context.statement()));
            
            if (context.ChildCount > 1)
            {
                statements.AddRange(VisitStatementList(context.statementList()));
            }

            return statements;
        }

        public StatementNode VisitStatement(DazelParser.StatementContext context)
        {
            if (context.GetType() == typeof(DazelParser.AssignmentContext))
            {
                return VisitAssignment(context.assignment());
            }

            if (context.GetType() == typeof(DazelParser.IfStatementContext))
            {
                return VisitIfStatement(context.ifStatement());
            }

            if (context.GetType() == typeof(DazelParser.RepeatLoopContext))
            {
                return VisitRepeatLoop(context.repeatLoop());
            }

            if (context.GetType() == typeof(DazelParser.ExpressionContext))
            {
                return new ExpressionVisitor().VisitFunctionInvocation(context.expression());
            }

            throw new ArgumentException("Invalid statement");
        }
        
        public AssignmentNode VisitAssignment(DazelParser.AssignmentContext context)
        {
            return new AssignmentNode();
        }
        public RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context)
        {
            // RepeatNode statements = VisitStatementList(context.statementList());

            return new RepeatNode();
        }

        public IfStatement VisitIfStatement(DazelParser.IfStatementContext context)
        {
            // TODO: update expression visitor
            var expression = new ExpressionVisitor().VisitExpression(context.expression());
            var statements = VisitStatementList(context.statementList());

            return new IfStatement();
        }

        public FunctionInvocation VisitFunctionInvocation(DazelParser.FunctionInvocationContext context)
        {
            var values = new ExpressionVisitor().VisitValueList(context.valueList());

            return new FunctionInvocation();
        }
    }
}