using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary
{
    public abstract class Function
    {
        public abstract int NumArguments { get; }
        private SymbolType ReturnType { get; }

        protected Function(SymbolType returnType)
        {
            ReturnType = returnType;
        }
        
        public abstract ValueNode Build(List<ValueNode> parameters);
    }
}