using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class SymbolTableBuilderTest
    {
        private AbstractSyntaxTree ast;
        private const string TestCodePath = "./dazel_test_code.txt";

        [SetUp]
        public void Setup()
        {
            ICharStream stream = CharStreams.fromPath(TestCodePath);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            IParseTree parseTree = new DazelParser(tokens) {BuildParseTree = true}.start();

            this.ast = new AstBuilder().BuildAst(parseTree);
        }
    }
}