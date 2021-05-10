using System.Collections.Generic;
using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class StatementBlockNode : StatementNode
    {
        public List<StatementNode> Statements;
        
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}