using Antlr4.Runtime.Tree;

namespace Interpreter
{
    public sealed class GameObjectContents : Node
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GameObjectContents(IParseTree gameObjectParseTreeNode) : base(gameObjectParseTreeNode)
        {
        }
    }
}