using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.EntitiesFunctions
{
    public sealed class SpawnEntityFunction : Function
    {
        public override int NumArguments => 2;

        protected override Action Call { get; }
        public SpawnEntityFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            //call();

            return null;
        }
    }
}