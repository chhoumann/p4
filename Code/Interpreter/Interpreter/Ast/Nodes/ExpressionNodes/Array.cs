using System;
using System.Collections.Generic;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class Array : Value
    {
        public List<Value> Values { get; set; }

        public override void PrintMe()
        {
            Console.Write("[");
            for (int i = 0; i < Values.Count; i++)
            {
                Value value = Values[i];
                value.PrintMe();
                if (i != Values.Count - 1)Console.Write(", ");
            }

            Console.Write("]");
        }
    }
}