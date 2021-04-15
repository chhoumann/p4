using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public sealed class AstBuilderTests
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
        public void fml()
        {
            new AstBuilder().BuildAst(parseTree);
        }
    }
}