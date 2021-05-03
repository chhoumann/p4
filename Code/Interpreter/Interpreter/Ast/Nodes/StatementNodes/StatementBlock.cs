using System.Collections.Generic;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    internal sealed class StatementBlock : StatementNode
    {
        public List<StatementNode> Statements;
        
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}