﻿using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;
using Dazel.Compiler.StandardLibrary;

namespace Dazel.Compiler.Ast.Nodes.StatementNodes
{
    public sealed class FunctionInvocationNode : StatementExpressionNode
    {
        public string Identifier { get; set; }
        public List<ValueNode> Parameters { get; set; }
        public SymbolType ReturnType { get; set; }
        public Function Function { get; private set; }

        public FunctionInvocationNode Setup()
        {
            if (DazelStdLib.TryGetFunction(Identifier, out Function function) && function.NumArguments == Parameters.Count)
            {
                Function = function;
                function.GetReturnType(Parameters);

                return this;
            }
            
            DazelLogger.EmitError($"{Identifier} function not found in Dazel Standard Library.", Token);
            return null;
        }
        
        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}