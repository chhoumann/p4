using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class LineFunction : Function
    {
        public override int NumArguments => 3;

        protected override Action Call { get; }
        public LineFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            // call();
            
            return null;
        }
    }
}