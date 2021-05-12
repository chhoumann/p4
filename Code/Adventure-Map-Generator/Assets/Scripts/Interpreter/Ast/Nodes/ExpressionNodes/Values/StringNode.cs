using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class StringNode : ValueNode
    {
        public string Value { get; set; }

        public StringNode()
        {
            Type = SymbolType.String;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}