﻿using System.Collections.Generic;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectContent : GameObjectNode
    {
        public List<Statement> Statements { get; set; }
    }
}