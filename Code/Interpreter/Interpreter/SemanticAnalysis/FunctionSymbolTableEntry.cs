using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace Interpreter.SemanticAnalysis
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