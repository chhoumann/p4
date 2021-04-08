using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public static class GlobalSymbolTable
    {
        public static List<SymbolTable> SymbolTables { get; } = new();
    }
}