using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public GameObject Root { get; }
        
        public AbstractSyntaxTree(IParseTree parseTree, IGameObjectVisitor visitor)
        {
            Root = visitor.VisitGameObject(parseTree.GetChild(0) as DazelParser.GameObjectContext);
        }
    }
}