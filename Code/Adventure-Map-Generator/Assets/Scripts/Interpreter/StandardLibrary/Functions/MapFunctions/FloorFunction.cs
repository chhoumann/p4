using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class FloorFunction : Function
    {
        public override int NumArguments => 1;
        
        public FloorFunction() : base(SymbolType.Void) { }

        public string TileName { get; private set; }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is StringNode stringNode)
            {
                TileName = stringNode.Value;
            }

            return null;
        }
    }
}