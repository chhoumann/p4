using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class EntitiesType : GameObjectContentType
    {
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}