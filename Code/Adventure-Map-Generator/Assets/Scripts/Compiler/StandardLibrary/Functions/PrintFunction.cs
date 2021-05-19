﻿using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;
using UnityEngine;

namespace Dazel.Compiler.StandardLibrary.Functions
{
    public sealed class PrintFunction : Function
    {
        public PrintFunction() : base(SymbolType.Void) { }

        public override int NumArguments => 1;

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            Debug.Log($"Patient0: {parameters[0]}");
            ValueNode = parameters[0];
            Debug.Log($"Given {ValueNode}");
            return null;
        }

        public void Log()
        {
            if (ValueNode is IdentifierValueNode identifierValueNode)
            {
                DazelLogger.EmitMessage(identifierValueNode.ValueNode.ToString(), ValueNode.Token);
            } else
            {
                DazelLogger.EmitMessage(ValueNode.ToString(), ValueNode.Token);
            }
        }
    }
}