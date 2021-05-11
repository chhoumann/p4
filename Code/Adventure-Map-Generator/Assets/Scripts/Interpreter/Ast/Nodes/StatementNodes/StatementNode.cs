using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.StatementNodes
{
    public abstract class StatementNode : Node<IStatementVisitor>
    {
    }
}