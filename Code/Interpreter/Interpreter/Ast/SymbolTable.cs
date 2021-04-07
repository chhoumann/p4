using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public class SymbolTable
    {
        public static List<Scope> Instance => Singleton.Value;
        private static readonly Lazy<List<Scope>> Singleton = new(new SymbolTable().Scopes);

        private List<Scope> Scopes { get; } = new();

        // public Scope BuildSymbolTable(Node astRoot)
        // {
        //     Scope scopeRoot; // = ProcessNode(astRoot);
        //     
        //     Scopes.Add(scopeRoot);
        //     
        //     return scopeRoot;
        // }
        //
        // private void ProcessNode(Node node)
        // {
        //     // if block (statementblock/gameobjectcontent/gameobject), openscope
        //     // if declaration, entersymbol and set isDeclaration to true
        //     // if reference, entersymbol and set isDeclaration to false
        // }

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
        public string Type { get; set; }
        private Scope Parent { get; }
        private List<Scope> Scopes { get; } = new();
        private readonly List<ScopeRow> values = new();

        public Scope(string identifier, Scope parent)
        {
            Identifier = identifier;
            Parent = parent;
        }

        public Scope OpenChildScope(string identifier = "")
        {
            Scope childScope = new(identifier, this);
            Scopes.Add(childScope);

            return childScope;
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
            foreach (ScopeRow value in values)
            {
                if (value.Identifier.Equals(identifier)) return value;
            }

            if (Parent != null) return Parent.RetrieveSymbol(identifier);
            throw new ArgumentException(identifier, $"Could not find symbol with identifier {identifier}");
        }

        public Boolean IsDeclaration(string identifier)
        {
            try
            {
                RetrieveSymbol(identifier);
                return false;
            }
            catch
            {
                return true;
            }
        }

        public void Print(int indentation)
        {
            Console.WriteLine(new string(' ', indentation * 2) + this);
            foreach (ScopeRow scopeRow in values)
            {
                Console.WriteLine(new string(' ', indentation * 3) + $"V: {scopeRow.Identifier} has type '{scopeRow.Type}'. IsDeclaration: {scopeRow.IsDeclaration}");
            }
            foreach (Scope scope in Scopes)
            {
                scope.Print(indentation + 2);
            }
            
        }

        public override string ToString()
        {
            return $"Scope: {Identifier} has type {Type}, {values.Count} values, and {Scopes.Count} children";
        }
    }
}