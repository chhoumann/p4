using System.Collections.Generic;

namespace Interpreter.SemanticAnalysis
{
    public abstract class SemanticAnalysis
    {
        protected readonly Stack<SymbolTable<SymbolTableEntry>> EnvironmentStack = new();

        protected void OpenScope()
        {
            SymbolTable<SymbolTableEntry> parentScope = EnvironmentStack.Count > 0 ? EnvironmentStack.Peek() : null;
            SymbolTable<SymbolTableEntry> newScope = new(parentScope);
            
            EnvironmentStack.Push(newScope);
        }

        protected void CloseScope()
        {
            EnvironmentStack.Pop();
        }
    }
}