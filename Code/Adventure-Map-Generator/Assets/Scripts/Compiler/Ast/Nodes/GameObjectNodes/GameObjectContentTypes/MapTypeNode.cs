using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class MapTypeNode : GameObjectContentTypeNode
    {
        public override ContentType ContentType => ContentType.Map;
        
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}