using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions
{
    public sealed class SizeFunction : Function
    {
        public override int NumArguments { get; }

        protected override Action Call { get; }
        public SizeFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            // call();
            
            return null;
        }
    }
}