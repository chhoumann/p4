using Antlr4.Runtime.Tree;

namespace Interpreter
{
    public sealed class GameObjectType : Node
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GameObjectType(IParseTree gameObjectParseTreeNode) : base(gameObjectParseTreeNode)
        {
        }
    }
}