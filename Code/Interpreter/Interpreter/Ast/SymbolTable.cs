using System;
using System.Collections.Generic;

namespace Interpreter.Ast
{
    public class SymbolTable
    {
        public static List<Scope> Instance => Singleton.Value;
        private static readonly Lazy<List<Scope>> Singleton = new(new SymbolTable().Scopes);

        private List<Scope> Scopes { get; } = new();

        public void RetrieveSymbol(string[] identifier)
        {
            foreach (Scope scope in Scopes)
            {
                if (scope.Identifier == identifier[0])
                {
                    // osv
                }
            }
        }
    }

    public class Scope
    {
        public string Identifier { get; set; }
        private List<Scope> Scopes { get; } = new();
        private readonly List<ScopeRow> values = new();

        public void OpenAdjacentScope(Scope scope)
        {
            SymbolTable.Instance.Add(scope);
        }

        public void OpenChildScope(Scope scope)
        {
            Scopes.Add(scope);
        }

        public void EnterSymbol(string identifier, ScopeRow data)
        {
            // values.Add(identifier, data);
        }

        public ScopeRow RetrieveSymbol(string identifier)
        {
            // if (values.ContainsKey(identifier))
            //     return values[identifier];

            ScopeRow symbol = null;
            
            foreach (Scope scope in Scopes)
            {
                symbol = scope.RetrieveSymbol(identifier);
            }

            return symbol;
        }
    }
}