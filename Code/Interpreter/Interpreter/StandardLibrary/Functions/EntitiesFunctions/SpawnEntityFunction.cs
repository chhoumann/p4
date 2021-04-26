using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.EntitiesFunctions
{
    public class SpawnEntityFunction : Function
    {
        public SpawnEntityFunction() : base(SymbolType.Void) { }

        private protected override Action call { get; }
        public override ValueNode Execute(List<ValueNode> parameters)
        {
            //call();

            return null;
        }
    }
}