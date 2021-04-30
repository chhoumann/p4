using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.SemanticAnalysis;
using NUnit.Framework;

namespace Tests.Semantics
{
    [TestFixture]
    internal class TypeCheckTests
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

            ast = new AstBuilder().BuildAst(parseTree);
        }

        [Test]
        public void TypeCheck_ExpectPass()
        {
            var tc = new TypeChecker(ast);
            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.That(tc.EnvironmentStack.Count == 0);
        }
        
        
        
    }
}