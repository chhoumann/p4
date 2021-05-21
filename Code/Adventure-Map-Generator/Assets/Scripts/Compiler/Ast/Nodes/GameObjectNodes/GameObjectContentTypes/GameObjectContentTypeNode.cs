using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public abstract class GameObjectContentTypeNode : Node<IGameObjectContentTypeVisitor>
    {
        public abstract ContentType ContentType { get; }
    }
}