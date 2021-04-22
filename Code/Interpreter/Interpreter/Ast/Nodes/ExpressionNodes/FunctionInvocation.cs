using System.Collections.Generic;
using Interpreter.Ast.Nodes.StatementNodes;
using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class FunctionInvocation : StatementExpression
    {
        public string Identifier { get; set; }
        public List<Value> Parameters { get; set; }
        public SymbolType ReturnType { get; set; }

        public Value Evaluate()
        {
            return new IdentifierValue()
            {
                Value = "Function Evaluate has not yet been implemented."
            };
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}