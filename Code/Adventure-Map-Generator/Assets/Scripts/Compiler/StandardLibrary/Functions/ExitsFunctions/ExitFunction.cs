using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ExitFunction : Function
    {
        public override int NumArguments => 2;

        public ExitFunction() : base(SymbolType.Void) { }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coords && parameters[1] is MemberAccessNode memberAccess)
            {
                ValueNode = new TileExitValueNode(coords.ToVector2());
                return ValueNode;
            }
            
            // TODO: Should return some exit type so we can assign to exits
            throw new ArgumentException("Invalid arguments passed to Exit function.");
        }
    }
}