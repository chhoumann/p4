﻿using System.Collections.Generic;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class Array : Value
    {
        public List<Value> Values { get; set; }
    }
}