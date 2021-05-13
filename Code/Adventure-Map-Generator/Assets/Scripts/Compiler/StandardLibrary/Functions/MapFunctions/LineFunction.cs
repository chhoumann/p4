using System.Collections.Generic;
using System.Numerics;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.MapFunctions
{
    public sealed class LineFunction : Function
    {
        public override int NumArguments => 3;

        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public LineFunction() : base(SymbolType.Void) { }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coords1 && parameters[1] is ArrayNode coords2)
            {
                Start = coords1.ToVector2();
                End = coords2.ToVector2();
            }
            
            return null;
        }
    }
}