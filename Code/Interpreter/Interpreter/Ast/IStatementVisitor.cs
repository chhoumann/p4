using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public interface IStatementVisitor
    {
        StatementNode VisitStatementList(DazelParser.StatementListContext context);
        StatementNode VisitStatement(DazelParser.StatementContext context);
        StatementNode VisitRepeatLoop(DazelParser.RepeatLoopContext context);
        StatementNode VisitIfStatement(DazelParser.IfStatementContext context);
        StatementNode VisitFunctionInvocation(DazelParser.FunctionInvocationContext context);
        StatementNode VisitAssignment(DazelParser.AssignmentContext context);
    }
}