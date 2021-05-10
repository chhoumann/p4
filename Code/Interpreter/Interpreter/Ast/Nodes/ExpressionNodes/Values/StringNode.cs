using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal class StringNode : ValueNode
    {
        public string Value { get; set; }

        public StringNode()
        {
            this.Type = SymbolType.String;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}