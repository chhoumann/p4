using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes
{
    internal abstract class StatementNode : Node<IStatementVisitor>
    {
    }
}