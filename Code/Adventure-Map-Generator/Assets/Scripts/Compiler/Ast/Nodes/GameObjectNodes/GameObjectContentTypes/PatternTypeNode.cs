using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class PatternTypeNode : GameObjectContentTypeNode
    {
        public override ContentType ContentType => ContentType.Pattern;
        
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}