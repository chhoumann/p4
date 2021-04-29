using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;
using Interpreter.SemanticAnalysis.API.Values;

namespace Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    internal sealed class ExitFunction : Function
    {
        public override int NumArguments => 2;

        public ExitFunction() : base(SymbolType.Void) { }

        public override ValueNode Build(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coords && parameters[1] is MemberAccess memberAccess)
            {
                return new ExitValue(coords.ToVector2());
            }
            
            // TODO: Should return some exit type so we can assign to exits
            throw new ArgumentException("Invalid arguments passed to Exit function.");
        }
    }
}