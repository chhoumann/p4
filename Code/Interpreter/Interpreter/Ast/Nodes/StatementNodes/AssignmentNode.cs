﻿using System;
using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class AssignmentNode : StatementExpression
    {
        public string Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
        public override void PrintMe()
        {
            Console.Write($"{Identifier} = ");
            Expression.PrintMe();
            Console.WriteLine();
        }
    }
}