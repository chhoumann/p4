using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public GameObject root;
        public AbstractSyntaxTree(IParseTree parseTree)
        {
            root = new GameObjectVisitor().VisitGameObject(parseTree.GetChild(0) as DazelParser.GameObjectContext);
            //gameObject.PrintMe();
        }
    }
}