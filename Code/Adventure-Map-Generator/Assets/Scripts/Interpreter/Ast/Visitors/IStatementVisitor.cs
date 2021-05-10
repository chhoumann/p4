using P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes;

namespace P4.MapGenerator.Interpreter.Ast.Visitors
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