using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions
{
    public sealed class SizeFunction : Function
    {
        public override int NumArguments => 2;

        public SizeFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            return null;
        }
    }
}