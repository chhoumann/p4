using System;
using System.Collections.Generic;

namespace P4.MapGenerator.Interpreter.SemanticAnalysis
{
    public sealed class SymbolTable<T>
    {
        private readonly Dictionary<string, T> symbols = new Dictionary<string, T>();
        private readonly SymbolTable<T> parent;

        public SymbolTable(SymbolTable<T> parent)
        {
            this.parent = parent;
        }

        public T RetrieveSymbol(string identifier)
        {
            if (symbols.TryGetValue(identifier, out T symbol))
            {
                return symbol;
            }
            
            if (parent != null)
            {
                T parentSymbol = parent.RetrieveSymbol(identifier);

                if (parentSymbol != null)
                {
                    return parentSymbol;
                }
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