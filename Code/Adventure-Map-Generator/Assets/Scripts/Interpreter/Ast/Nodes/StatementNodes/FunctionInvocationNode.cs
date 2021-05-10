using System;
using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.SemanticAnalysis;
using Dazel.Interpreter.StandardLibrary;

namespace Dazel.Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class FunctionInvocationNode : StatementExpressionNode
    {
        public string Identifier { get; set; }
        public List<ValueNode> Parameters { get; set; }
        public SymbolType ReturnType { get; set; }

        public ValueNode Create()
        {
            if (DazelStdLib.Functions.TryGetValue(Identifier, out Function function) && function.NumArguments == Parameters.Count)
            {
                return function.Build(Parameters);
            }
            
            // TODO: This is not the right exception. This should be called from within execute. Create a new exception type.
            throw new ArgumentException($"{Identifier} function not found in Dazel Standard Library.");
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}