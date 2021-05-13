using Dazel.Compiler.Ast.Nodes.StatementNodes;

namespace Dazel.Compiler.Ast.Visitors
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