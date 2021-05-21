using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.SemanticAnalysis
{
    public abstract class EnvironmentStore
    {
        protected abstract string GameObjectIdentifier { get; set; }
        
        public Stack<SymbolTable> EnvironmentStack { get; } = new Stack<SymbolTable>();
        public static int TopSymbolTablesCount => TopSymbolTables.Count;

        private static readonly Dictionary<string, SymbolTable> TopSymbolTables = new Dictionary<string, SymbolTable>();
        
        protected SymbolTable CurrentScope;
        
        protected void OpenScope()
        {
            SymbolTable parentScope = EnvironmentStack.Count > 0 ? EnvironmentStack.Peek() : null;
            SymbolTable newScope = new SymbolTable(parentScope);
            
            parentScope?.Children.Add(newScope);
            
            if (parentScope == null && !TopSymbolTables.ContainsKey(GameObjectIdentifier))
            {
                TopSymbolTables.Add(GameObjectIdentifier, newScope);
            }
            
            EnvironmentStack.Push(newScope);
            CurrentScope = newScope;
        }

        protected void CloseScope()
        {
            CurrentScope = CurrentScope.Parent;
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
            
            SymbolTable symbolTable = TopSymbolTables[identifierList[0]];
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