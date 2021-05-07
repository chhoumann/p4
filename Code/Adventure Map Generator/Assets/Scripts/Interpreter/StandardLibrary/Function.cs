using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.StandardLibrary
{
    internal abstract class Function
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