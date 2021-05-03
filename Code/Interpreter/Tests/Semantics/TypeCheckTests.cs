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
        public void TypeCheck_ExpectPass()
        {
            var ast = new AstBuilder().BuildAst(parseTree);
            var tc = new TypeChecker(ast);
            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.That(tc.EnvironmentStack.Count == 0);
        }
        
        [Test]
        public void TypeCheck_ExpectaPass()
        {
            var ast = new AstBuilder().BuildAst(parseTree);
            var tc = new TypeChecker(ast);
            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.That(tc.EnvironmentStack.Count == 0);
        }
        
        
        
    }
}