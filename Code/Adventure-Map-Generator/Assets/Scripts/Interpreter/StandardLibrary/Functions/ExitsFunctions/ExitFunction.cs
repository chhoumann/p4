using System;
using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ExitFunction : Function
    {
        public override int NumArguments => 2;

        public ExitFunction() : base(SymbolType.Void) { }

        public override ValueNode Build(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coords && parameters[1] is MemberAccessNode memberAccess)
            {
                return new ExitValueNode(coords.ToVector2());
            }
            
            // TODO: Should return some exit type so we can assign to exits
            throw new ArgumentException("Invalid arguments passed to Exit function.");
        }
    }
}