using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class FunctionInvocation : StatementExpression
    {
        public string Identifier { get; set; }
        public List<Value> Parameters { get; set; }

        public Value Evaluate()
        {
            return new IdentifierValue()
            {
                Value = "Evaluate has not yet been implemented."
            };
        }

        public override void PrintMe()
        {
            Console.Write($"{Identifier}(");
            for (int index = 0; index < Parameters.Count; index++)
            {
                Value parameter = Parameters[index];
                parameter.PrintMe();
                if (index != Parameters.Count - 1) Console.Write(", ");
            }

            Console.Write(")\n");
            base.PrintMe();
        }
    }
}