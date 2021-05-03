using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    internal abstract class StatementNode : Node<IStatementVisitor>
    {
    }
}