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
        ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context);
    }
}