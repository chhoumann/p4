using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class FloatValueNode : ValueNode
    {
        public override SymbolType Type => SymbolType.Float;
        
        public float Value { get; set; }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}