using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;
using UnityEngine;

namespace Dazel.Compiler.SemanticAnalysis
{
    public abstract class EnvironmentStore
    {
        protected abstract string GameObjectIdentifier { get; set; }
        
        public Stack<SymbolTable> EnvironmentStack { get; } = new Stack<SymbolTable>();
        public static int TopSymbolTablesCount => topSymbolTables.Count;

        public static IReadOnlyDictionary<string, SymbolTable> TopSymbolTables => topSymbolTables;
        private static readonly Dictionary<string, SymbolTable> topSymbolTables = new Dictionary<string, SymbolTable>();
        
        protected SymbolTable CurrentTopScope;
        
        protected void OpenScope()
        {
            SymbolTable parentScope = EnvironmentStack.Count > 0 ? CurrentTopScope : null;
            SymbolTable newScope = new SymbolTable(parentScope);
            
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
            CurrentTopScope = EnvironmentStack.Pop();
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

            SymbolTable symbolTable = topSymbolTables[identifierList[0]];
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