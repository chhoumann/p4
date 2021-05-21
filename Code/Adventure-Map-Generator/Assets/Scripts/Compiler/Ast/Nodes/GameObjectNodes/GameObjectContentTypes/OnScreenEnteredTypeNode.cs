using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class OnScreenEnteredTypeNode : GameObjectContentTypeNode
    {
        public override ContentType ContentType => ContentType.OnScreenEntered;
        
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}