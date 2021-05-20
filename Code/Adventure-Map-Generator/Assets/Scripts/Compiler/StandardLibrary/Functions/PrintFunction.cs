﻿using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions
{
    public sealed class PrintFunction : Function
    {
        public PrintFunction() : base(SymbolType.Void) { }

        public override int NumArguments => 1;
        
        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            ValueNode = parameters[0];

            return null;
        }

        public void Log()
        {
            if (ValueNode is IdentifierValueNode identifierValueNode)
            {
                if (CurrentSymbolTable.symbols.TryGetValue(identifierValueNode.Identifier, out SymbolTableEntry symbolTableEntry))
                {
                    DazelLogger.EmitMessage(symbolTableEntry.ToString(), ValueNode.Token);
                }
                else
                {
                    DazelLogger.EmitWarning("null", ValueNode.Token);
                }
            }
            else
            {
                DazelLogger.EmitMessage(ValueNode.ToString(), ValueNode.Token);
            }
        }
    }
}