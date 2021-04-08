using System;
using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public sealed class Scope : IScopeRow
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
        private Scope Parent { get; }
        private readonly List<IScopeRow> values = new();

        public Scope(string identifier, Scope parent)
        {
            Identifier = identifier;
            Parent = parent;
        }

        public Scope OpenChildScope(string identifier = "")
        {
            Scope nestedScope = new(identifier, this);

            values.Add(nestedScope);

            return nestedScope;
        }

        public void EnterSymbol(ScopeRow data)
        {
            values.Add(data);
        }

        /// <summary>
        /// Looks for identifier in current scope and all parent scopes
        /// </summary>
        /// <param name="identifier">Identifier to look for</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if no identifier with given name found</exception>
        public ScopeRow RetrieveSymbol(string identifier)
        {
            foreach (IScopeRow value in values)
            {
                if (value is ScopeRow row && row.Identifier.Equals(identifier))
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
            
            foreach (IScopeRow scopeRow in values)
            {
                Console.Write(new string(' ', indentation));
                switch (scopeRow)
                {
                    case ScopeRow row:
                        Console.WriteLine(new string(' ', indentation + 2) + $"ID: {row}");
                        break;
                    case Scope scope:
                        scope.Print(indentation + 2);
                        break;
                }
            }
        }

        public override string ToString()
        {
            int numNestedScopes = values.FindAll(s => s is Scope).Count;
            return $"Scope: {Identifier ?? "Statement Scope"} has type {(Type == null ? "" : Type.Name)}: {values.Count - numNestedScopes} values and {numNestedScopes} scopes.";
        }
    }
}