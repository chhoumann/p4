using System.Collections.Generic;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class MemberAccessNode : ValueNode
    {
        public override SymbolType Type => SymbolType.MemberAccess;
        
        public readonly List<string> Identifiers = new List<string>();

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return string.Join(".", Identifiers);
        }
    }
}