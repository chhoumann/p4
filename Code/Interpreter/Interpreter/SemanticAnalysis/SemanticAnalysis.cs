using System;
using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public class SemanticAnalysis
    {
        private Stack<SymbolTable> environmentStack;
        
        
    }

    public sealed class SymbolTable<T>
    {
        private readonly Dictionary<string, T> symbols = new();

        public T RetrieveSymbol(string identifier)
        {
            if (symbols.TryGetValue(identifier, out T symbol))
            {
                return symbol;
            }
            
            throw new ArgumentException($"Invalid identifier: {identifier}");
        }

        public void AddOrUpdateSymbol(string identifier, T data)
        {
            if (symbols.ContainsKey(identifier))
            {
                symbols[identifier] = data;
                return;
            }
            
            symbols.Add(identifier, data);
        }
    }
}