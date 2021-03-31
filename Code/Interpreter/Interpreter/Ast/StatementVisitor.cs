using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public sealed class StatementVisitor : DazelBaseVisitor<StatementNode>, IStatementVisitor
    {
        public override StatementNode VisitStatementList(DazelParser.StatementListContext context)
        {
            if (context.ChildCount == 1)
            {
                return VisitStatement(context.statement());
            }

            return new StatementList();
        }

        public override StatementNode VisitStatement(DazelParser.StatementContext context)
        {
            if (context.GetType() == typeof(DazelParser.RepeatLoopContext))
            {
                return VisitRepeatLoop(context.repeatLoop());
            }
            else if (context.GetType() == typeof(DazelParser.FunctionInvocationContext))
            {
                return VisitFunctionInvocation(context.functionInvocation());
            }
            else if (context.GetType() == typeof(DazelParser.AssignmentContext))
            {
                return VisitAssignment(context.assignment());
            }
            
            return VisitIfStatement(context.ifStatement());
        }
        
        public override StatementNode VisitAssignment(DazelParser.AssignmentContext context)
        {
            StatementNode eval = VisitExpression(context.expression());
            
            return new AssignmentNode();
        }
        public override StatementNode VisitRepeatLoop(DazelParser.RepeatLoopContext context)
        {
            StatementNode statements = VisitStatementList(context.statementList());

            return new RepeatNode();
        }

        public override StatementNode VisitIfStatement(DazelParser.IfStatementContext context)
        {
            // TODO: update expression visitor
            var expression = new ExpressionVisitor().VisitExpression(context.expression());
            var statements = VisitStatementList(context.statementList());

            return new IfStatement();
        }

        public override StatementNode VisitFunctionInvocation(DazelParser.FunctionInvocationContext context)
        {
            var values = new ExpressionVisitor().VisitValueList(context.valueList());

            return new FunctionInvocation();
        }
    }
}