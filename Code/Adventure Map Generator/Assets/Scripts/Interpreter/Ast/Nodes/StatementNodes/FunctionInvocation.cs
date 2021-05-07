using System;
using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.Ast.Visitors;
using P4.MapGenerator.Interpreter.SemanticAnalysis;
using P4.MapGenerator.Interpreter.StandardLibrary;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes
{
    internal sealed class FunctionInvocation : StatementExpression
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