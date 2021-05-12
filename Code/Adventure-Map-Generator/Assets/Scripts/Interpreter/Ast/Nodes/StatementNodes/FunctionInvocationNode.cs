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
        public Function Function { get; private set; }

        public ValueNode Create()
        {
            if (DazelStdLib.TryGetFunction(Identifier, out Function function) && function.NumArguments == Parameters.Count)
            {
                Function = function;
                return function.GetValueType(Parameters);
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