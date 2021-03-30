using Interpreter;

namespace Interpreter
{
    public abstract class Node : INode
    {
        public abstract void Accept(IVisitor visitor);
    }


public class GameObject : Node
{
    private Node value;

    public GameObject(Node value)
    {
        this.value = value;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new System.NotImplementedException();
    }
}

public class GameObjectType : Node
{
    private Node value;

    public GameObjectType(Node value)
    {
        this.value = value;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new System.NotImplementedException();
    }
}


}