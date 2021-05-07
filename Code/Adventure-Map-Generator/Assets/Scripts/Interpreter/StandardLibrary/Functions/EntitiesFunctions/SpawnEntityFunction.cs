using System;
using System.Collections.Generic;
using System.Numerics;
using P4.MapGenerator.Interpreter.Ast;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.StandardLibrary.Functions.EntitiesFunctions
{
    public sealed class SpawnEntityFunction : Function
    {
        public override int NumArguments => 2;
        public Vector2 SpawnPoint { get; private set; }
        public DGameObject Entity { get; set; }

        public SpawnEntityFunction() : base(SymbolType.Void) { }

        public override ValueNode Build(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coordsArray && parameters[1] is IdentifierValue id)
            {
                SpawnPoint = coordsArray.ToVector2();

                if (AbstractSyntaxTree.Instance.TryRetrieveGameObject(id.Value, out DGameObject gameObject))
                {
                    Entity = gameObject;
                }
            }
            
            throw new ArgumentException("Invalid arguments to SpawnEntity function");
        }
    }
}