using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class SquareFunction : Function
    {
        public override int NumArguments => 3;

        public SquareFunction() : base(SymbolType.Void) { }

        public override ValueNode Build(List<ValueNode> parameters)
        {
            return null;
        }
    }
}