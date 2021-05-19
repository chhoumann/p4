using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;
using UnityEngine;

namespace Dazel.Compiler.SemanticAnalysis
{
    public abstract class EnvironmentStore
    {
        protected abstract string GameObjectIdentifier { get; set; }
        
        public Stack<SymbolTable<SymbolTableEntry>> EnvironmentStack { get; } = new Stack<SymbolTable<SymbolTableEntry>>();
        public static int TopSymbolTablesCount => topSymbolTables.Count;

        public static IReadOnlyDictionary<string, SymbolTable<SymbolTableEntry>> TopSymbolTables => topSymbolTables;
        private static readonly Dictionary<string, SymbolTable<SymbolTableEntry>> topSymbolTables = new Dictionary<string, SymbolTable<SymbolTableEntry>>();
        
        protected SymbolTable<SymbolTableEntry> CurrentTopScope;
        
        protected void OpenScope()
        {
            SymbolTable<SymbolTableEntry> parentScope = EnvironmentStack.Count > 0 ? CurrentTopScope : null;
            SymbolTable<SymbolTableEntry> newScope = new SymbolTable<SymbolTableEntry>(parentScope);
            
            EnvironmentStack.Push(newScope);

            parentScope?.Children.Add(newScope);

            if (!topSymbolTables.ContainsKey(GameObjectIdentifier))
            {
                topSymbolTables.Add(GameObjectIdentifier, newScope);
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
            /*Debug.Log($"Looking for {symbolIdentifier} in {identifierList[0]}");

            Debug.Log(topSymbolTables[identifierList[0]]);
            Debug.Log(topSymbolTables[identifierList[0]].Children.Count);

            foreach (var child in topSymbolTables[identifierList[0]].Children)
            {
                foreach (KeyValuePair<string, SymbolTableEntry> childSymbol in child.symbols)
                {
                    Debug.Log(childSymbol.Key);
                    Debug.Log(childSymbol.Value);
                }
            }*/

            SymbolTable<SymbolTableEntry> symbolTable = topSymbolTables[identifierList[0]];
            SymbolTableEntry symbolTableEntry = symbolTable.RetrieveSymbolInChildScope(symbolIdentifier);

            if (symbolTableEntry is VariableSymbolTableEntry variableSymbolTableEntry)
            {
                return variableSymbolTableEntry;
            }
            
            return null;
        }
        
        public static void CleanUp() => topSymbolTables.Clear();
    }
}