using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;
using UnityEngine;

namespace Dazel.Compiler.StandardLibrary
{
    public sealed class PrintFunction : Function
    {
        public PrintFunction() : base(SymbolType.Void) { }

        public override int NumArguments => 1;

        private ValueNode valueNode;
        
        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            valueNode = parameters[0];
            return null;
        }

        public override void PostAstExecute()
        {
            Debug.Log(valueNode.ToString());
        }
    }
}