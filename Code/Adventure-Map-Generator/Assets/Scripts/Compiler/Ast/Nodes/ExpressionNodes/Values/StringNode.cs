using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class StringNode : ValueNode
    {
        public override SymbolType Type => SymbolType.String;
        
        public string Value { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}