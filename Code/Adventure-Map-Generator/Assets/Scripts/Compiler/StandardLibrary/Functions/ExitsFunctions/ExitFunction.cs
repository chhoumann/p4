using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ExitFunction : Function
    {
        public override int NumArguments => 2;

        public ExitFunction() : base(SymbolType.Exit) { }

        public MemberAccessNode MemberAccessNode { get; private set; }
        
        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coords && parameters[1] is MemberAccessNode memberAccess)
            {
                ValueNode = new TileExitValueNode(coords.ToVector2(), memberAccess)
                {
                    Token = memberAccess.Token
                };
                
                MemberAccessNode = memberAccess;
                return ValueNode;
            }
            
            InvalidArgumentsException(parameters);

            return null;
        }
    }
}