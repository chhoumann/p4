using System;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class FactorOperation : OperationNode
    {
        public override void PrintMe()
        {
            Console.Write($" {Operation} ");
        }
    }
}