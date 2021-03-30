using Antlr4.Runtime.Tree;

namespace Interpreter
{
    public sealed class GameObject : Node
    {
        public string Identifier { get; private set; }
        public string Type { get; private set; }

        public IParseTree Contents { get; private set; } 

        private readonly IParseTree parseTreeNode;

        public GameObject(IParseTree gameObjectParseTreeNode) : base(gameObjectParseTreeNode)
        {
            parseTreeNode = gameObjectParseTreeNode;
        }
        
        public void Accept(IVisitor visitor)
        {
            Type = parseTreeNode.GetChild(0).GetText();
            Identifier = parseTreeNode.GetChild(1).GetText();
            Contents = parseTreeNode.GetChild(3);
            
            visitor.Visit(this);
        }
    }
}