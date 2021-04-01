using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public interface IStatementVisitor
    {
        List<StatementNode> VisitStatementList(DazelParser.StatementListContext context);
        StatementNode VisitStatement(DazelParser.StatementContext context);
        RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context);
        IfStatement VisitIfStatement(DazelParser.IfStatementContext context);
        StatementExpression VisitStatementExpression(DazelParser.StatementExpressionContext context);
        FunctionInvocation VisitFunctionInvocation(DazelParser.FunctionInvocationContext context);
    }
}