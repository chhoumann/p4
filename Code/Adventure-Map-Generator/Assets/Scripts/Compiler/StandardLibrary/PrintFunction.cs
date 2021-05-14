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

        public string Output { get; set; }
        
        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            Output = parameters[0].ToString();
            Debug.Log(Output);
            return null;
        }
    }
}