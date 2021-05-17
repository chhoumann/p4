using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ScreenExitFunction : Function
    {
        public override int NumArguments => 2;
        
        public ScreenExitFunction() : base(SymbolType.Exit) { }

        public Direction ExitDirection { get; private set; }

        public string ConnectedScreenIdentifier { get; private set; }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is IdentifierValueNode dirId && parameters[1] is IdentifierValueNode screenId
                && Enum.TryParse(dirId.Identifier, false, out Direction exitDirection))
            {
                ExitDirection = exitDirection;
                ConnectedScreenIdentifier = screenId.Identifier;

                return new ScreenExitValueNode(ConnectedScreenIdentifier, exitDirection)
                {
                    Token = dirId.Token
                };
            }

            InvalidArgumentsException(parameters);

            return null;
        }
    }
}