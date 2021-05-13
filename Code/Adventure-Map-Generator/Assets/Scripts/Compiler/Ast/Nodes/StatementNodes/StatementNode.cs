using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.StatementNodes
{
    public abstract class StatementNode : Node<IStatementVisitor>
    {
    }
}