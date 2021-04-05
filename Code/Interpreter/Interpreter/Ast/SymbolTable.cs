using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast
{
    public sealed class SymbolTable
    {
        public static SymbolTable Instance => Singleton.Value;
        private SymbolTable Parent { get; set; }
        private List<SymbolTable> Children { get; set; } = new();

        public Dictionary<string, Value> Values { get; set; } = new();

        private static readonly Lazy<SymbolTable> Singleton = new(() => new SymbolTable());

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