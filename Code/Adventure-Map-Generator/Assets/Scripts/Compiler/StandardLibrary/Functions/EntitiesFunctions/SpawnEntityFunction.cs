using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;
using Vector2 = System.Numerics.Vector2;

namespace Dazel.Compiler.StandardLibrary.Functions.EntitiesFunctions
{
    public sealed class SpawnEntityFunction : Function
    {
        public override int NumArguments => 2;

        public Vector2 SpawnPosition { get; private set; }
        public string EntityIdentifier { get; private set; }
        
        public SpawnEntityFunction() : base(SymbolType.Void) { }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is StringNode stringNode && parameters[1] is ArrayNode arrayNode)
            {
                EntityIdentifier = stringNode.Value;
                SpawnPosition = arrayNode.ToVector2();
                return null;
            }

            throw InvalidArgumentsException(parameters);
        }
    }
}