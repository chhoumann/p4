using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.SemanticAnalysis
{
    public abstract class EnvironmentStore
    {
        protected abstract string GameObjectIdentifier { get; set; }
        
        public Stack<SymbolTable<SymbolTableEntry>> EnvironmentStack { get; } = new Stack<SymbolTable<SymbolTableEntry>>();
        public static int TopSymbolTablesCount => TopSymbolTables.Count;

        private static Dictionary<string, SymbolTable<SymbolTableEntry>> TopSymbolTables { get; } = new Dictionary<string, SymbolTable<SymbolTableEntry>>();
        
        protected SymbolTable<SymbolTableEntry> CurrentTopScope;
        
        protected void OpenScope()
        {
            SymbolTable<SymbolTableEntry> parentScope = EnvironmentStack.Count > 0 ? EnvironmentStack.Peek() : null;
            SymbolTable<SymbolTableEntry> newScope = new SymbolTable<SymbolTableEntry>(parentScope);
            
            EnvironmentStack.Push(newScope);

            parentScope?.Children.Add(newScope);

            if (parentScope == null && !TopSymbolTables.ContainsKey(GameObjectIdentifier))
            {
                TopSymbolTables.Add(GameObjectIdentifier, newScope);
            }
            
            CurrentTopScope = newScope;
        }

        protected void CloseScope()
        {
            CurrentTopScope = CurrentTopScope.Parent;
        }

        public static VariableSymbolTableEntry AccessMember(MemberAccessNode memberAccessNode)
        {
            try
            {
                return AccessMember(memberAccessNode.Identifiers);
            }
            catch 
            {
                DazelLogger.EmitError($"Member not found in {memberAccessNode}", memberAccessNode.Token);
            }

            return null;
        }
        
        public static VariableSymbolTableEntry AccessMember(List<string> identifierList)
        {
            string symbolIdentifier = identifierList[identifierList.Count - 1];
            
            SymbolTable<SymbolTableEntry> symbolTable = TopSymbolTables[identifierList[0]];
            SymbolTableEntry symbolTableEntry = symbolTable.RetrieveSymbolInChildScope(symbolIdentifier);

            if (symbolTableEntry is VariableSymbolTableEntry variableSymbolTableEntry)
            {
                return variableSymbolTableEntry;
            }
            
            return null;
        }
        
        public static void CleanUp() => TopSymbolTables.Clear();
    }
}