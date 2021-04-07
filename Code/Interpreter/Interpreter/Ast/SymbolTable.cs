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
}