using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Compiler.SemanticAnalysis
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