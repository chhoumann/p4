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

            if (context.statementList() != null)
            {
                statements.AddRange(VisitStatementList(context.statementList()));
            }

            return statements;
        }

        public StatementNode VisitStatement(DazelParser.StatementContext context)
        {
            var child = context.GetChild(0);
            if (child.GetType() == typeof(DazelParser.IfStatementContext))
            {
                return VisitIfStatement(context.ifStatement());
            }

            if (child.GetType() == typeof(DazelParser.RepeatLoopContext))
            {
                return VisitRepeatLoop(context.repeatLoop());
            }

            if (child.GetType() == typeof(DazelParser.StatementExpressionContext))
            {
                return VisitStatementExpression(context.statementExpression());
            }

            throw new ArgumentException("Invalid statement");
        }
        
        public RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context)
        {
            // RepeatNode statements = VisitStatementList(context.statementList());

            return new RepeatNode();
        }

        public IfStatement VisitIfStatement(DazelParser.IfStatementContext context)
        {
            // // TODO: update expression visitor
            // var expression = new ExpressionVisitor().VisitExpression(context.expression());
            // var statements = VisitStatementList(context.statementList());

            return new IfStatement();
        }

        public StatementExpression VisitStatementExpression(DazelParser.StatementExpressionContext context)
        {
            return new StatementExpression();
        }

        public FunctionInvocation VisitFunctionInvocation(DazelParser.FunctionInvocationContext context)
        {
            //var values = new ExpressionVisitor().VisitValueList(context.valueList());

            return new FunctionInvocation();
        }
    }
}