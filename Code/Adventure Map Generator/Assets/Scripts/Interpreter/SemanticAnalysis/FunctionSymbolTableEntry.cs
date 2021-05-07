using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace P4.MapGenerator.Interpreter.SemanticAnalysis
{
    internal class FunctionSymbolTableEntry : SymbolTableEntry
    {
        private readonly List<ValueNode> parameters;

        public FunctionSymbolTableEntry(SymbolType type, List<ValueNode> parameters) : base(type)
        {
            this.parameters = parameters;
        }
    }
}