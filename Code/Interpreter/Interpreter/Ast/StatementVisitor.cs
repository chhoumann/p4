using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class StatementVisitor : IStatementVisitor
    {
        public List<StatementNode> VisitStatementList(DazelParser.StatementListContext context)
        {
            List<StatementNode> statements = new();
            
            if (context.statementBlock() != null)
            {
                statements.AddRange(VisitStatementBlock(context.statementBlock()));                
            }
            
            if (context.statement() != null)
            {
                statements.Add(VisitStatement(context.statement()));
            }

            if (context.statementList() != null)
            {
                statements.AddRange(VisitStatementList(context.statementList()));
            }

            return statements;
        }

        public StatementNode VisitStatement(DazelParser.StatementContext context)
        {
            IParseTree child = context.GetChild(0);
            
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
        
        // TOOD: Undecided
        public RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context)
        {
            // RepeatNode statements = VisitStatementList(context.statementList());

            return new RepeatNode();
        }

        // TOOD: Undecided
        public IfStatement VisitIfStatement(DazelParser.IfStatementContext context)
        {
            // // TODO: update expression visitor
            // var expression = new ExpressionVisitor().VisitExpression(context.expression());
            // var statements = VisitStatementList(context.statementList());

            return new IfStatement();
        }

        public StatementExpression VisitStatementExpression(DazelParser.StatementExpressionContext context)
        {
            if (context.assignment() != null)
            {
                return VisitAssignment(context.assignment());
            }

            return VisitFunctionInvocation(context.functionInvocation());
        }

        public FunctionInvocation VisitFunctionInvocation(DazelParser.FunctionInvocationContext context)
        {
            return new()
            {
                Identifier = context.IDENTIFIER().GetText(),
                Parameters = new ExpressionVisitor().VisitValueList(context.valueList()),
            };
        }

        public StatementExpression VisitAssignment(DazelParser.AssignmentContext context)
        {
            return new AssignmentNode()
            {
                Identifier = context.IDENTIFIER().GetText(),
                Expression = new ExpressionVisitor().VisitExpression(context.expression())
            };
        }

        public List<StatementNode> VisitStatementBlock(DazelParser.StatementBlockContext context)
        {
            if (context.statementList().statementBlock() != null)
            {
                return VisitStatementBlock(context.statementList().statementBlock());
            }
            
            return VisitStatementList(context.statementList());
        }
    }
}