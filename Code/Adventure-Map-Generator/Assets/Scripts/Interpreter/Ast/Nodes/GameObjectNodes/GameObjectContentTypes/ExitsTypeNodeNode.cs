using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class ExitsTypeNodeNode : GameObjectContentTypeNode
    {
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}