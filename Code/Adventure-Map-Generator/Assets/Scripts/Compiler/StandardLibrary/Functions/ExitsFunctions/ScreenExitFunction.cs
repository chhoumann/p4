﻿using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ScreenExitFunction : Function
    {
        public override int NumArguments => 2;
        
        public ScreenExitFunction() : base(SymbolType.Void) { }

        public Direction ExitDirection { get; private set; }
        public GameObjectNode ConnectedScreen { get; private set; }

        private string connectedScreenName;

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is IdentifierValueNode dirId && parameters[1] is IdentifierValueNode screenId
                && Enum.TryParse(dirId.Value, false, out Direction exitDirection))
            {
                // TODO: Should return some exit type so we can assign to exits 
                ExitDirection = exitDirection;
                connectedScreenName = screenId.Value;
                return null;
            }

            throw InvalidArgumentsException(parameters);
        }

        public override ValueNode Setup(List<ValueNode> parameters, AbstractSyntaxTree ast)
        {
            if (ast.TryRetrieveGameObject(connectedScreenName, out GameObjectNode screen))
            {
                ConnectedScreen = screen;
            }
            
            return null;
        }
    }
}