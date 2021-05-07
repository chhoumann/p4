using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal sealed class IntValue : ValueNode
    {
        public int Value { get; set; }

        public IntValue()
        {
            this.Type = SymbolType.Integer;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}