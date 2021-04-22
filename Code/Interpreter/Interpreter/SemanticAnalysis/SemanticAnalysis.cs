using System;
using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public class SemanticAnalysis
    {
        private Stack<SymbolTable> environmentStack;
        
        
    }

    public sealed class SymbolTable
    {
        private readonly Dictionary<string, string> symbols = new();

        public string RetrieveSymbol(string identifier)
        {
            if (symbols.TryGetValue(identifier, out string symbol))
            {
                return symbol;
            }
            
            throw new ArgumentException($"Invalid identifier: {identifier}");
        }
    }
}