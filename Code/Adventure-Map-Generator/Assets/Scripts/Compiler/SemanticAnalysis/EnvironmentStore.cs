using System.Collections.Generic;

namespace Dazel.Compiler.SemanticAnalysis
{
    public abstract class EnvironmentStore
    {
        public readonly Stack<SymbolTable<SymbolTableEntry>> EnvironmentStack = new Stack<SymbolTable<SymbolTableEntry>>();

        protected void OpenScope()
        {
            SymbolTable<SymbolTableEntry> parentScope = EnvironmentStack.Count > 0 ? EnvironmentStack.Peek() : null;
            SymbolTable<SymbolTableEntry> newScope = new SymbolTable<SymbolTableEntry>(parentScope);
            
            EnvironmentStack.Push(newScope);
        }

        protected void CloseScope()
        {
            EnvironmentStack.Pop();
        }
    }
}