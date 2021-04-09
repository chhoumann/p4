using System.Collections.Generic;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public class StatementBlock : StatementNode
    {
        public List<StatementNode> Statements;
        
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}