using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public abstract class ExitValueNode : ValueNode
    {
        public override SymbolType Type => SymbolType.Exit;

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}