using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast
{
    public sealed class SymbolTables
    {
        public static SymbolTables Instance => Singleton.Value;

        public List<SymbolTable> Tables { get; set; }

        private static readonly Lazy<SymbolTables> Singleton = new(() => new SymbolTables());

        public sealed class SymbolTable
        {
            private SymbolTable Parent { get; set; }
            private List<SymbolTable> Children { get; set; } = new();

            public Dictionary<string, Value> Values { get; set; } = new();
            
            private SymbolTable() { }

            public SymbolTable OpenAdjacentScope()
            {
                return Parent.OpenChildScope();
            }

            public SymbolTable OpenChildScope()
            {
                SymbolTable childTable = new();
                Children.Add(childTable);
                return childTable;
            }
        }
    }
}