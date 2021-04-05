using System;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class TerminalExpression : ExpressionNode
    {
        public ExpressionNode Child { get; set; }
        public override void PrintMe()
        {
            Child.PrintMe();
        }
    }
}