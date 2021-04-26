using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class WallsFunction : Function
    {
        public override int NumArguments => 1;

        protected override Action Call { get; }
        public WallsFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            // call();

            return null;
        }
    }
}