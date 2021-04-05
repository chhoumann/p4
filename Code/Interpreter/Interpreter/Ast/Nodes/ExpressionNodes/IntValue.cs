using System;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class IntValue : Value
    {
        public int Value { get; set; }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            Console.Write(Value);
        }
    }
}