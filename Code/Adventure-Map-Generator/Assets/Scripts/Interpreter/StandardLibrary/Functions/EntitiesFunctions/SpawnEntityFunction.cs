using System;
using System.Collections.Generic;
using System.Text;
using Dazel.Interpreter.Ast;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.SemanticAnalysis;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

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
            if (parameters[0] is StringNode entityName && parameters[1] is ArrayNode coordsArray)
            {
                SpawnPoint = coordsArray.ToVector2();

                Debug.Log(parameters[0]);
                Debug.Log(parameters[1]);

                if (AbstractSyntaxTree.Instance.TryRetrieveGameObject(entityName.Value, out GameObjectNode gameObject))
                {
                    Entity = gameObject;
                }

                return null;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Invalid arguments to SpawnEntity function:");
            
            foreach (ValueNode valueNode in parameters)
            {
                sb.AppendLine($"{valueNode}");
            }
            
            throw new ArgumentException(sb.ToString());
        }
    }
}