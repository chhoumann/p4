using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public GameObject Root { get; }
        
        public AbstractSyntaxTree(IParseTree parseTree)
        {
            Root = new GameObjectVisitor().VisitGameObject(parseTree.GetChild(0) as DazelParser.GameObjectContext);
        }
    }
}