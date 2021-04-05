using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast
{
    public sealed class SymbolTable
    {
        public static SymbolTable Instance => Singleton.Value;
        
        public Dictionary<string, Value> Values { get; set; } = new Dictionary<string, Value>();
        public List<SymbolTable> SymbolTables { get; set; } = new List<SymbolTable>();

        private static readonly Lazy<SymbolTable> Singleton = new(() => new SymbolTable());

        private int scopeCounter;

        public void OpenScope()
        {
            scopeCounter++;
        }

        public void CloseScope()
        {
            scopeCounter--;
        }
    }
}