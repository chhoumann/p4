using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;
using UnityEngine;

namespace Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ExitFunction : Function
    {
        public override int NumArguments => 2;

        public ExitFunction() : base(SymbolType.Exit) { }

        public  MemberAccessNode memberAccessNode { get; private set; }
        
        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            if (parameters[0] is ArrayNode coords && parameters[1] is MemberAccessNode memberAccess)
            {
                ValueNode = new TileExitValueNode(coords.ToVector2(), memberAccess);
                memberAccessNode = memberAccess;
                return ValueNode;
            }
            
            throw InvalidArgumentsException(parameters);
        }
    }
}