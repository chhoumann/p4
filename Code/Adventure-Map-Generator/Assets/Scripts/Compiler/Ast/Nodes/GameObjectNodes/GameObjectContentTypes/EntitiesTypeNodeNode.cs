using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class EntitiesTypeNodeNode : GameObjectContentTypeNode
    {
        public override ContentType ContentType => ContentType.Entities;
        
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}