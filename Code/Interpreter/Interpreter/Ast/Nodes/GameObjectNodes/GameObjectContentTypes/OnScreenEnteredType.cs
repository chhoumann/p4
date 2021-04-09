namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class OnScreenEnteredType : GameObjectContentType
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}