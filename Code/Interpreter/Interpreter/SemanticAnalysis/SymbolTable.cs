using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public class SymbolTable
    {
        public static SymbolTable Instance => Singleton;
        private static readonly SymbolTable Singleton = new();

        public List<Scope> Scopes { get; } = new();

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