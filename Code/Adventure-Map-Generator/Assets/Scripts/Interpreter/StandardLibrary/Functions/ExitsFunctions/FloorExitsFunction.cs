using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    public class FloorExitsFunction : Function
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