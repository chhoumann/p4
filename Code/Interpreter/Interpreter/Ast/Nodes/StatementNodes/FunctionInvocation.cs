using System.Collections.Generic;
using Interpreter.Ast.Nodes.ValueNodes;
using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public sealed class FunctionInvocation : StatementExpression
    {
        public string Identifier { get; set; }
        public List<ValueNode> Parameters { get; set; }
        public SymbolType ReturnType { get; set; }

        public ValueNode Evaluate()
        {
            return new IdentifierValue
            {
                Value = "Function Evaluate has not yet been implemented."
            };
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}