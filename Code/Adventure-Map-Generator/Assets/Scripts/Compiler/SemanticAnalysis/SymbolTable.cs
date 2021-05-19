using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class SymbolTable<T>
    {
        public readonly Dictionary<string, T> symbols = new Dictionary<string, T>();
        
        public SymbolTable<T> Parent { get; }
        public List<SymbolTable<T>> Children { get; } = new List<SymbolTable<T>>();

        public SymbolTable(SymbolTable<T> parent)
        {
            Parent = parent;
        }

        public T RetrieveSymbolInParentScope(string identifier)
        {
            if (symbols.TryGetValue(identifier, out T symbol))
            {
                return symbol;
            }
            
            if (Parent != null)
            {
                T parentSymbol = Parent.RetrieveSymbolInParentScope(identifier);

                if (parentSymbol != null)
                {
                    return parentSymbol;
                }
            }

            throw new ArgumentException($"Invalid identifier: {identifier}.");
        }
        
        public T RetrieveSymbolInChildScope(string identifier)
        {
            if (symbols.TryGetValue(identifier, out T symbol))
            {
                return symbol;
            }

            if (Children.Count > 0)
            {
                foreach (SymbolTable<T> childSymbolTable in Children)
                {
                    T childSymbol = childSymbolTable.RetrieveSymbolInChildScope(identifier);

                    if (childSymbol != null)
                    {
                        return childSymbol;
                    }
                }
            }

            throw new ArgumentException($"Invalid identifier: {identifier}.");
        }
        
        public SymbolTable<T> RetrieveSymbolTable(string symbolIdentifier)
        {
            if (symbols.ContainsKey(symbolIdentifier))
            {
                return this;
            }

            SymbolTable<T> parentSymbolTable = Parent?.RetrieveSymbolTable(symbolIdentifier);

            return parentSymbolTable;
        }

        public void AddOrUpdateSymbol(string identifier, T data)
        {
            if (symbols.ContainsKey(identifier))
            {
                symbols[identifier] = data;
                return;
            }

            SymbolTable<T> symbolTable = RetrieveSymbolTable(identifier);

            if (symbolTable != null)
            {
                symbolTable.AddOrUpdateSymbol(identifier, data);
            }
            else
            {
                symbols.Add(identifier, data);
            }
        }
    }
}