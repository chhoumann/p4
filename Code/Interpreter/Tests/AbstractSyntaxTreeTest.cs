using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.GameObjectNodes;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AbstractSyntaxTreeTest
    {
        private IParseTree parseTree;
        private readonly string parseThisYouFilthyCasual = "./dazel_test_code.txt"; 

        [SetUp]
        public void Setup()
        {
            ICharStream stream = CharStreams.fromPath(parseThisYouFilthyCasual);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            parseTree = (new DazelParser(tokens) {BuildParseTree = true}).start();
        }
        
        
        [Test]
        public void AbstractSyntaxTree_Create_VisitGameObject()
        {
            // Arrange
            IGameObjectVisitor visitor = Substitute.For<IGameObjectVisitor>();
            
            AbstractSyntaxTree ast = new(parseTree, visitor);

            var context = parseTree.GetChild(0) as DazelParser.GameObjectContext;
            
            // Assert
            visitor
                .Received()
                .VisitGameObject(context);
        }
    }
}