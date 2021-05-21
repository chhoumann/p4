using System;
using System.Collections.Generic;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class SymbolTable
    {
        public readonly Dictionary<string, SymbolTableEntry> symbols = new Dictionary<string, SymbolTableEntry>();
        
        public SymbolTable Parent { get; }
        public List<SymbolTable> Children { get; } = new List<SymbolTable>();

        public SymbolTable(SymbolTable parent)
        {
            Parent = parent;
        }

        public SymbolTableEntry RetrieveSymbolInParentScope(string identifier)
        {
            if (symbols.TryGetValue(identifier, out SymbolTableEntry symbol))
            {
                return symbol;
            }
            
            if (Parent != null)
            {
                SymbolTableEntry parentSymbol = Parent.RetrieveSymbolInParentScope(identifier);

                if (parentSymbol != null)
                {
                    return parentSymbol;
                }
            }

            throw new ArgumentException($"Invalid identifier: {identifier}.");
        }
        
        public SymbolTableEntry RetrieveSymbolInChildScope(string identifier)
        {
            if (symbols.TryGetValue(identifier, out SymbolTableEntry symbol))
            {
                return symbol;
            }

            if (Children.Count > 0)
            {
                foreach (SymbolTable childSymbolTable in Children)
                {
                    SymbolTableEntry childSymbol = childSymbolTable.RetrieveSymbolInChildScope(identifier);

                    if (childSymbol != null)
                    {
                        return childSymbol;
                    }
                }
            }

            throw new ArgumentException($"Invalid identifier: {identifier}.");
        }
        
        public SymbolTable RetrieveSymbolTable(string symbolIdentifier)
        {
            if (symbols.ContainsKey(symbolIdentifier))
            {
                return this;
            }

            SymbolTable parentSymbolTable = Parent?.RetrieveSymbolTable(symbolIdentifier);

            return parentSymbolTable;
        }

        public void AddOrUpdateSymbol(string identifier, SymbolTableEntry data)
        {
            if (symbols.ContainsKey(identifier))
            {
                symbols[identifier] = data;
                return;
            }

            SymbolTable symbolTable = RetrieveSymbolTable(identifier);

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