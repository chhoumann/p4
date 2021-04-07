namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class EntitiesType : GameObjectContentType
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}