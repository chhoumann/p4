using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes
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