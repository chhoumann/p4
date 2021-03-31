using System.Collections.Generic;
using System.Threading;
using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.StatementNodes;

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
        public override ExpressionNode VisitAssignment(DazelParser.AssignmentContext context)
        {
            ExpressionNode eval = VisitExpression(context.expression());
            
            return new AssignmentNode();
        }
        
        public override ExpressionNode VisitExpression(DazelParser.ExpressionContext context)
        {
            return VisitSumExpression(context.sumExpression());
        }

        public override ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context)
        {
            if (context.GetType() == typeof(DazelParser.FactorExpressionContext))
            {
                return VisitFactorExpression(context.factorExpression());
            }

            DazelParser.SumExpressionContext sumExpression = context.sumExpression();
            return new SumExpression();
        }
        
       public override ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context)
        {
            if (context.GetType() == typeof(DazelParser.TerminalExpressionContext))
            {
                return VisitTerminalExpression(context.terminalExpression());
            }

            DazelParser.FactorExpressionContext factorExpression = context.factorExpression();
            return new FactorExpression();
        }
        
        public override ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context)
        {
            if (context.GetType() == typeof(DazelParser.ValueContext))
            {
                return VisitValue(context.value());
            }

            DazelParser.ExpressionContext expression = context.expression();
            return VisitExpression(expression);
        }
        
        public override ExpressionNode VisitValue(DazelParser.ValueContext context)
        {
            if (context.GetType() == typeof(DazelParser.ArrayContext))
            {
                return VisitArray(context.array());
            }
            else if(context.GetType() == typeof(DazelParser.FunctionInvocationContext))
            {
                return VisitFunctionInvocation(context.functionInvocation());
            }
            else if (context.GetType() == typeof(DazelParser.MemberAccessContext))
            {
                return VisitMemberAccess(context.memberAccess());
            }
            
            return new Value();
        }
        
        public override ExpressionNode VisitArray(DazelParser.ArrayContext context)
        {
            if (context.GetType() == typeof(DazelParser.ValueListContext))
            {
                return VisitValueList(context.valueList());
            }

            return new Array();
        }
        
        public override ExpressionNode VisitValueList(DazelParser.ValueListContext context)
        {
            if (context.ChildCount == 1)
            {
                return new Value();
            }

            return new ValueList();
        }
        
        public override ExpressionNode VisitFactorOperation(DazelParser.FactorOperationContext context)
        {
            return new FactorOperation();
        }
        
        public override ExpressionNode VisitSumOperation(DazelParser.SumOperationContext context)
        {
            return new SumOperation();
        }
        
        public override ExpressionNode VisitMemberAccess(DazelParser.MemberAccessContext context)
        {
            return new MemberAccess();
        }
    }
}