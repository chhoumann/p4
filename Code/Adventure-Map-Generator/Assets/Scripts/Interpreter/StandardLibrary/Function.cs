using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary
{
    public abstract class Function
    {
        public abstract int NumArguments { get; }
        private SymbolType ReturnType { get; }

        protected ValueNode ValueNode;

        protected Function(SymbolType returnType)
        {
            ReturnType = returnType;
        }
        
        public abstract ValueNode GetReturnType(List<ValueNode> parameters);
        public virtual ValueNode Setup(List<ValueNode> parameters) => null;
    }
}