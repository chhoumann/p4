using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions
{
    public class FloorExitsFunction : Function
    {
        public override int NumArguments => 2;
        
        public FloorExitsFunction() : base(SymbolType.Void) { }
        
        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            // TODO: Should return some exit type so we can assign to exits
            return null;
        }
    }
}