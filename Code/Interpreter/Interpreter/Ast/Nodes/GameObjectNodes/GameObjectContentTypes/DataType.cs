namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class DataType : GameObjectContentType
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}