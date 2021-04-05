using System;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class IdentifierValue : Value
    {
        public string Value { get; set; }

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