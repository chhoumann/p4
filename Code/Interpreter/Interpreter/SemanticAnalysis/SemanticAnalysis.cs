using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public abstract class SemanticAnalysis
    {
        protected readonly Stack<SymbolTable<SymbolTableEntry>> EnvironmentStack = new();
    }
}