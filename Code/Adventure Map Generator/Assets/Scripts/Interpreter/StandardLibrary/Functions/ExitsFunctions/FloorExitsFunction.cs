using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    internal class FloorExitsFunction : Function
    {
        public override int NumArguments => 2;
        
        public FloorExitsFunction() : base(SymbolType.Void) { }
        
        public override ValueNode Build(List<ValueNode> parameters)
        {
            // TODO: Should return some exit type so we can assign to exits
            return null;
        }
    }
}