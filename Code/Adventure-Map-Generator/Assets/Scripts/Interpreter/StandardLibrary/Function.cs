using System;
using System.Collections.Generic;
using System.Text;
using Dazel.Interpreter.Ast;
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
        public virtual ValueNode Setup(List<ValueNode> parameters, AbstractSyntaxTree ast) => null;

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