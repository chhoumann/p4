using System;
using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.SemanticAnalysis;
using UnityEngine;

namespace Dazel.Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class SizeFunction : Function
    {
        public override int NumArguments => 2;
        
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SizeFunction() : base(SymbolType.Void) { }

        public override ValueNode GetValueType(List<ValueNode> parameters)
        {
            Debug.Log("aaaaaaaaaaaaaaa");

            foreach (ValueNode valueNode in parameters)
            {
                if (valueNode.Type != SymbolType.Integer)
                {
                    throw new ArgumentException("Invalid type.");
                }
            }

            if (parameters[0] is IntValueNode width && parameters[1] is IntValueNode height)
            {
                Width = width.Value;
                Height = height.Value;
            }

            return null;
        }
    }
}