﻿using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.MapFunctions
{
    public sealed class FloorFunction : Function
    {
        public override int NumArguments => 1;
        
        public FloorFunction() : base(SymbolType.Void) { }

        public override ValueNode Execute(List<ValueNode> parameters)
        {
            throw new NotImplementedException();
        }
    }
}