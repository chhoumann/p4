using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.SemanticAnalysis
{
    public class FunctionSymbolTableEntry : SymbolTableEntry
    {
        private readonly Value[] parameters;

        public FunctionSymbolTableEntry(string identifier, SymbolType type, Value[] parameters) : base(identifier, type)
        {
            this.parameters = parameters;
        }
    }
}