using System;
using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class FloorFunction : Function
    {
        public override int NumArguments => 1;
        
        public FloorFunction() : base(SymbolType.Void) { }

        public override ValueNode Build(List<ValueNode> parameters)
        {
            throw new NotImplementedException();
        }
    }
}