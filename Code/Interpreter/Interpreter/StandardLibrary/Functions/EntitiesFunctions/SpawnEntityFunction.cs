using System;
using System.Collections.Generic;
using System.Numerics;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.EntitiesFunctions
{
    public sealed class SpawnEntityFunction : Function
    {
        public override int NumArguments => 2;
        public Vector2 SpawnPoint { get; private set; }
        public GameObject Entity { get; set; }

        public SpawnEntityFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coordsArray && parameters[1] is IdentifierValue id)
            {
                SpawnPoint = coordsArray.ToVector2();

                if (AbstractSyntaxTree.Instance.TryRetrieveGameObject(id.Value, out GameObject gameObject))
                {
                    Entity = gameObject;
                }
            }
            
            throw new ArgumentException("Invalid arguments to SpawnEntity function");
        }
    }
}