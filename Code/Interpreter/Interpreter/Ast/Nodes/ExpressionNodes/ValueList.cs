using System.Collections.Generic;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class ValueList : ExpressionNode
    {
        public List<Value> Values { get; set; }
    }
}