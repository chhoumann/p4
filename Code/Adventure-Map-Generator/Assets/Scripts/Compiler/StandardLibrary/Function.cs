using System;
using System.Collections.Generic;
using System.Text;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary
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
        
        public virtual void PostAstExecute() { }

        protected ArgumentException InvalidArgumentsException(IEnumerable<ValueNode> parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Invalid arguments in function {GetType()}:");
            
            foreach (ValueNode valueNode in parameters)
            {
                sb.AppendLine($"{valueNode}");
            }
            
            throw new ArgumentException(sb.ToString());
        }
    }
}