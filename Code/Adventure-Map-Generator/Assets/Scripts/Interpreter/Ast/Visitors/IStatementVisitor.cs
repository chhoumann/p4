using Dazel.Interpreter.Ast.Nodes.StatementNodes;

namespace Dazel.Interpreter.Ast.Visitors
{
    public interface IStatementVisitor
    {
        void Visit(StatementBlockNode statementBlockNode);
        void Visit(IfStatementNode ifStatementNode);
        void Visit(RepeatNode repeatNode);
        void Visit(AssignmentNode assignmentNode);
        void Visit(FunctionInvocationNode functionInvocationNode);
    }
}