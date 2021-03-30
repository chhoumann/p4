using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast
{
    public interface IExpressionVisitor
    {
        ExpressionNode VisitExpression(DazelParser.ExpressionContext context);
        ExpressionNode VisitAssignment(DazelParser.AssignmentContext context);
        ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context);
        ExpressionNode VisitFactorOperation(DazelParser.FactorOperationContext context);
        ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context);
        ExpressionNode VisitSumOperation(DazelParser.SumOperationContext context);
        ExpressionNode VisitArray(DazelParser.ArrayContext context);
        ExpressionNode VisitMemberAccess(DazelParser.MemberAccessContext context);
        ExpressionNode VisitValue(DazelParser.ValueContext context);
        ExpressionNode VisitValueList(DazelParser.ValueListContext context);
        ExpressionNode VisitTerminal(ITerminalNode node);
        ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context);
    }

    public sealed class ExpressionVisitor : DazelBaseVisitor<ExpressionNode>, IExpressionVisitor
    {
        public override ExpressionNode VisitExpression(DazelParser.ExpressionContext context)
        {
            return VisitSumExpression(context.sumExpression());
            //return base.VisitExpression(context);
        }

        public override ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context)
        {
            if (context.GetType() == typeof(DazelParser.FactorExpressionContext))
            {
                return VisitFactorExpression(context.factorExpression());
            }

            DazelParser.SumExpressionContext sumExpression = context.sumExpression();
            return new SumExpression();

            //return base.VisitSum_expression(context);
        }

        
        public override ExpressionNode VisitAssignment(DazelParser.AssignmentContext context)
        {
            return base.VisitAssignment(context);
        }

        public override ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context)
        {
            return base.VisitFactorExpression(context);
        }

        public override ExpressionNode VisitFactorOperation(DazelParser.FactorOperationContext context)
        {
            return base.VisitFactorOperation(context);
        }
        
        public override ExpressionNode VisitSumOperation(DazelParser.SumOperationContext context)
        {
            return base.VisitSumOperation(context);
        }

        public override ExpressionNode VisitArray(DazelParser.ArrayContext context)
        {
            return base.VisitArray(context);
        }

        public override ExpressionNode VisitMemberAccess(DazelParser.MemberAccessContext context)
        {
            return base.VisitMemberAccess(context);
        }

        public override ExpressionNode VisitValue(DazelParser.ValueContext context)
        {
            return base.VisitValue(context);
        }

        public override ExpressionNode VisitValueList(DazelParser.ValueListContext context)
        {
            return base.VisitValueList(context);
        }

        public override ExpressionNode VisitTerminal(ITerminalNode node)
        {
            return base.VisitTerminal(node);
        }

        public override ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context)
        {
            return base.VisitTerminalExpression(context);
        }
    }
}