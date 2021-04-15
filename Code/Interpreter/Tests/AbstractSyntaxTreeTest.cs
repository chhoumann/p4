using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AbstractSyntaxTreeTest
    {
        private IParseTree parseTree;
        private const string TestCodePath = "./dazel_test_code.txt";

        [SetUp]
        public void Setup()
        {
            ICharStream stream = CharStreams.fromPath(TestCodePath);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            parseTree = new DazelParser(tokens) {BuildParseTree = true}.start();
        }
        
        [Test]
        public void AbstractSyntaxTree_Create_VisitGameObject()
        {
            // Arrange
            IAstBuilder astBuilder = Substitute.For<IAstBuilder>();
            astBuilder.BuildAst(parseTree);

            // Assert
            astBuilder.Received().BuildAst(parseTree);
        }
    }
}