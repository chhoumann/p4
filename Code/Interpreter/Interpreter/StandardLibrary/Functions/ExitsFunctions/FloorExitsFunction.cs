using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    internal class FloorExitsFunction : Function
    {
        public override int NumArguments => 2;
        
        public FloorExitsFunction() : base(SymbolType.Void) { }
        
        public override ValueNode Execute(List<ValueNode> parameters)
        {
            // TODO: Should return some exit type so we can assign to exits
            return null;
        }
    }
}