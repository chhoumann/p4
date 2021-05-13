using System.Collections.Generic;
using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.StatementNodes
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