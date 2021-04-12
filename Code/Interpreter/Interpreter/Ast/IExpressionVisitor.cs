using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast
{
    public interface IExpressionVisitor
    {
        ExpressionNode VisitExpression(DazelParser.ExpressionContext context);
        ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context);
        ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context);
        ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context);
        Value VisitValue(DazelParser.ValueContext context);
        Array VisitArray(DazelParser.ArrayContext context);
        List<Value> VisitValueList(DazelParser.ValueListContext context);
        FactorOperation VisitFactorOperation(DazelParser.FactorOperationContext context);
        SumOperation VisitSumOperation(DazelParser.SumOperationContext context);
        MemberAccess VisitMemberAccess(DazelParser.MemberAccessContext context);
    }
}