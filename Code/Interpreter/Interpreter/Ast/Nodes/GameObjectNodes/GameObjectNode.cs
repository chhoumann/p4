namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public abstract class GameObjectNode
    {
        public virtual void Accept(IGameObjectVisitor visitor) { }
    }
}