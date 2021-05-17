using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class IdentifierValueNode : ValueNode
    {
        public override SymbolType Type => SymbolType.Identifier; 
        
        public string Identifier { get; set; }
        public ValueNode ValueNode { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{Identifier} ({Type})";
        }
    }
}