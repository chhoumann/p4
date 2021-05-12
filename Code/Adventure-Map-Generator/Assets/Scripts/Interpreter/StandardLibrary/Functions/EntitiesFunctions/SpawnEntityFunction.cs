using System;
using System.Collections.Generic;
using System.Numerics;
using Dazel.Interpreter.Ast;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.StandardLibrary.Functions.EntitiesFunctions
{
    public sealed class SpawnEntityFunction : Function
    {
        public override int NumArguments => 2;
        public Vector2 SpawnPoint { get; private set; }
        public GameObjectNode Entity { get; set; }

        public SpawnEntityFunction() : base(SymbolType.Void) { }

        public override ValueNode GetValueType(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coordsArray && parameters[1] is IdentifierValueNode id)
            {
                SpawnPoint = coordsArray.ToVector2();

                if (AbstractSyntaxTree.Instance.TryRetrieveGameObject(id.Value, out GameObjectNode gameObject))
                {
                    Entity = gameObject;
                }
            }
            
            throw new ArgumentException("Invalid arguments to SpawnEntity function");
        }
    }
}