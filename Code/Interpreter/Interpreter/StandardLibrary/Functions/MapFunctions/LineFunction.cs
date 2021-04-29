using System.Collections.Generic;
using System.Numerics;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.MapFunctions
{
    internal sealed class LineFunction : Function
    {
        public override int NumArguments => 3;

        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public LineFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
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