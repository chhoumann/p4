using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary
{
    public abstract class Function
    {
        private protected abstract Action call { get; }

        private SymbolType ReturnType { get; }

        protected Function(SymbolType returnType)
        {
            ReturnType = returnType;
        }
        
        public abstract ValueNode Execute(List<ValueNode> parameters);
    }
}