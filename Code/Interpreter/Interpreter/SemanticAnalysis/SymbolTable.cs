using System;
using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public sealed class SymbolTable : ISymbolTableEntry
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
        
        private SymbolTable Parent { get; }
        private readonly List<ISymbolTableEntry> values = new();

        public SymbolTable(string identifier, SymbolTable parent)
        {
            Identifier = identifier;
            Parent = parent;
        }

        public SymbolTable OpenChildScope(string identifier = "")
        {
            SymbolTable nestedSymbolTable = new(identifier, this);

            values.Add(nestedSymbolTable);

            return nestedSymbolTable;
        }

        public void EnterSymbol(SymbolTableEntry data)
        {
            values.Add(data);
        }

        /// <summary>
        /// Looks for identifier in current scope and all parent scopes
        /// </summary>
        /// <param name="identifier">Identifier to look for</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if no identifier with given name found</exception>
        public SymbolTableEntry RetrieveSymbol(string identifier)
        {
            foreach (ISymbolTableEntry value in values)
            {
                if (value is SymbolTableEntry row && row.Identifier.Equals(identifier))
                {
                    return row;
                }
            }

            return Parent?.RetrieveSymbol(identifier);
        }

        public bool IsDeclaration(string identifier)
        {
            return RetrieveSymbol(identifier) == null;
        }

        public void Print(int indentation)
        {
            Console.WriteLine(new string(' ', indentation) + this);
            
            foreach (ISymbolTableEntry scopeRow in values)
            {
                Console.Write(new string(' ', indentation));
                switch (scopeRow)
                {
                    case SymbolTableEntry row:
                        Console.WriteLine(new string(' ', indentation + 2) + $"ID: {row}");
                        break;
                    case SymbolTable scope:
                        scope.Print(indentation + 2);
                        break;
                }
            }
        }

        public override string ToString()
        {
            int numNestedScopes = values.FindAll(s => s is SymbolTable).Count;
            return $"Scope: {Identifier ?? "Statement Scope"} has type {(Type == null ? "" : Type.Name)}: {values.Count - numNestedScopes} values and {numNestedScopes} scopes.";
        }
    }
}