using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class ExitsTypeNodeNode : GameObjectContentTypeNode
    {
        public override ContentType ContentType => ContentType.Exits;
        
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}