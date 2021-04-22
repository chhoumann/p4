using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public class SemanticAnalysis
    {
        private Stack<SymbolTable> environmentStack;
        
        
    }

    public sealed class SymbolTable
    {
        public Dictionary<string, string> symbols { get; } = new();

        public string RetrieveSymbol()
        {
            return symbols.
        }
    }
}