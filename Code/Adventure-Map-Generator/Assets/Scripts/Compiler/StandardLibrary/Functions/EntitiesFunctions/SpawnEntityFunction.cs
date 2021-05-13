using System.Collections.Generic;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;
using Vector2 = System.Numerics.Vector2;

namespace Dazel.Compiler.StandardLibrary.Functions.EntitiesFunctions
{
    public sealed class SpawnEntityFunction : Function
    {
        public override int NumArguments => 2;
        public Vector2 SpawnPoint { get; private set; }
        public GameObjectNode Entity { get; set; }

        private string entityName;
        
        public SpawnEntityFunction() : base(SymbolType.Void) { }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is StringNode stringNode && parameters[1] is ArrayNode arrayNode)
            {
                entityName = stringNode.Value;
                SpawnPoint = arrayNode.ToVector2();
                return null;
            }

            throw InvalidArgumentsException(parameters);
        }

        public override ValueNode Setup(List<ValueNode> parameters, AbstractSyntaxTree ast)
        {
            if (ast.TryRetrieveGameObject(entityName, out GameObjectNode gameObject))
            {
                Entity = gameObject;
            }
            
            return null;
        }
    }
}