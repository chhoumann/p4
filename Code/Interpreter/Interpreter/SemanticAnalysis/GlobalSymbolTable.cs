using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public static class GlobalSymbolTable
    {
        public static Dictionary<string, SymbolTable> SymbolTables { get; } = new();
    }
}