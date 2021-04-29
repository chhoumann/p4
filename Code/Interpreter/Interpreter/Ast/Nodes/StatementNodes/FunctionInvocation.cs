using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis;
using Interpreter.StandardLibrary;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    internal sealed class FunctionInvocation : StatementExpression
    {
        public string Identifier { get; set; }
        public List<ValueNode> Parameters { get; set; }
        public SymbolType ReturnType { get; set; }

        public ValueNode Evaluate()
        {
            if (DazelStdLib.Functions.TryGetValue(Identifier, out Function function) && function.NumArguments == Parameters.Count)
            {
                return function.Execute(Parameters);
            }
            
            // TODO: This is not the right exception. This should be called from within execute. Create a new exception type.
            throw new ArgumentException($"{Identifier} called with wrong arguments: {string.Join(',', Parameters)}");
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}