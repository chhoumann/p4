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
            return new IntValue();
        }
    }
}