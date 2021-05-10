using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class IntValueNode : ValueNode
    {
        public int Value { get; set; }

        public IntValueNode()
        {
            Type = SymbolType.Integer;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}