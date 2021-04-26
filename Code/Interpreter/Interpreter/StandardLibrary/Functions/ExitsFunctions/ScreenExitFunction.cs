using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    public class ScreenExitFunction : Function
    {
        public override int NumArguments => 2;

        protected override Action Call { get; }
        
        public ScreenExitFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            // call();
    
            // TODO: Should return some exit type so we can assign to exits
            return null;
        }
    }
}