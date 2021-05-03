using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    internal sealed class DataType : GameObjectContentType
    {
        public override void Accept(IGameObjectContentTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}