using System;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class MemberAccess : Value
    {
        public string Left { get; set; }
        public string Right { get; set; }

        public override void PrintMe()
        {
            Console.WriteLine($"Left: {Left}, Right: {Right}");
        }
    }
}