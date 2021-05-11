using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Interpreter.SemanticAnalysis
{
    public class FunctionSymbolTableEntry : SymbolTableEntry
    {
        private readonly List<ValueNode> parameters;

        public FunctionSymbolTableEntry(SymbolType type, List<ValueNode> parameters) : base(type)
        {
            this.parameters = parameters;
        }
    }
}