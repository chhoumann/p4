using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class SquareFunction : Function
    {
        public override int NumArguments => 3;

        public SquareFunction() : base(SymbolType.Void) { }

        public override ValueNode Build(List<ValueNode> parameters)
        {
            return null;
        }
    }
}