using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast
{
    public interface IExpressionVisitor
    {
        ExpressionNode VisitExpression(DazelParser.ExpressionContext context);
        ExpressionNode VisitAssignment(DazelParser.AssignmentContext context);
        ExpressionNode VisitFactor_expression(DazelParser.Factor_expressionContext context);
        ExpressionNode VisitFactor_operation(DazelParser.Factor_operationContext context);
        ExpressionNode VisitSum_expression(DazelParser.Sum_expressionContext context);
        ExpressionNode VisitSum_operation(DazelParser.Sum_operationContext context);
        ExpressionNode VisitArray(DazelParser.ArrayContext context);
        ExpressionNode VisitMember_access(DazelParser.Member_accessContext context);
        ExpressionNode VisitValue(DazelParser.ValueContext context);
        ExpressionNode VisitValue_list(DazelParser.Value_listContext context);
        ExpressionNode VisitTerminal(ITerminalNode node);
        ExpressionNode VisitTerminal_expression(DazelParser.Terminal_expressionContext context);
    }

    public sealed class ExpressionVisitor : DazelBaseVisitor<ExpressionNode>, IExpressionVisitor
    {
        public override ExpressionNode VisitExpression(DazelParser.ExpressionContext context)
        {
            return VisitSum_expression(context.sum_expression());
            //return base.VisitExpression(context);
        }

        public override ExpressionNode VisitSum_expression(DazelParser.Sum_expressionContext context)
        {
            if (context.GetType() == typeof(DazelParser.Factor_expressionContext))
            {
                return VisitFactor_expression(context.factor_expression());
            }

            DazelParser.Sum_expressionContext sumExpression = context.sum_expression();
            return new SumExpression();

            //return base.VisitSum_expression(context);
        }

        
        public override ExpressionNode VisitAssignment(DazelParser.AssignmentContext context)
        {
            return base.VisitAssignment(context);
        }

        public override ExpressionNode VisitFactor_expression(DazelParser.Factor_expressionContext context)
        {
            return base.VisitFactor_expression(context);
        }

        public override ExpressionNode VisitFactor_operation(DazelParser.Factor_operationContext context)
        {
            return base.VisitFactor_operation(context);
        }
        
        public override ExpressionNode VisitSum_operation(DazelParser.Sum_operationContext context)
        {
            return base.VisitSum_operation(context);
        }

        public override ExpressionNode VisitArray(DazelParser.ArrayContext context)
        {
            return base.VisitArray(context);
        }

        public override ExpressionNode VisitMember_access(DazelParser.Member_accessContext context)
        {
            return base.VisitMember_access(context);
        }

        public override ExpressionNode VisitValue(DazelParser.ValueContext context)
        {
            return base.VisitValue(context);
        }

        public override ExpressionNode VisitValue_list(DazelParser.Value_listContext context)
        {
            return base.VisitValue_list(context);
        }

        public override ExpressionNode VisitTerminal(ITerminalNode node)
        {
            return base.VisitTerminal(node);
        }

        public override ExpressionNode VisitTerminal_expression(DazelParser.Terminal_expressionContext context)
        {
            return base.VisitTerminal_expression(context);
        }
    }
}