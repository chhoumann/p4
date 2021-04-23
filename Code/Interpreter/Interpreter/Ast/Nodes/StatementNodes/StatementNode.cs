using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public abstract class StatementNode : Node<IStatementVisitor>
    {
    }
}