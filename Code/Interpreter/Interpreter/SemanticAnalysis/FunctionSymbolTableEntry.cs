using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.SemanticAnalysis
{
    public class FunctionSymbolTableEntry : SymbolTableEntry
    {
        private readonly List<Value> parameters;

        public FunctionSymbolTableEntry(SymbolType type, List<Value> parameters) : base(type)
        {
            this.parameters = parameters;
        }
    }
}