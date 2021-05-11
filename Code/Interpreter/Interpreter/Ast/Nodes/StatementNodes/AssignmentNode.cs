using System;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    internal sealed class AssignmentNode : StatementExpression
    {
        public string Identifier { get; set; }
        public ExpressionNode Expression { get; set; }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}