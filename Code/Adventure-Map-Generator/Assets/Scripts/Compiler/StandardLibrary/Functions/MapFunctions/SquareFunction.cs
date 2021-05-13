using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.MapFunctions
{
    public sealed class SquareFunction : Function
    {
        public override int NumArguments => 3;

        public SquareFunction() : base(SymbolType.Void) { }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            return null;
        }
    }
}