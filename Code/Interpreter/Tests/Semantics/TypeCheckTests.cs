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

        private const string TestCode =
            "Screen SampleScreen1" + // Test GameObject
            "{" +
            "   Map" + // Test GameObjectContent & GameObjectContentType
            "   {" +
            "       Size(30, 24);" + // Test function invocation
            "       SomeVar1 = 2.0;" + // Test assignment & floats
            "       { " + // Test statement block
            "           SomeVar2 = 3 + 3 / 3;" + // Test expressions & integers
            "           x = SomeVar2;" + // Test identifier values
            "       }" +
            "       let = SampleScreen1.Exits.exit1;" + // Test member access 
            "       arr = [1, 2, 3];" + // Test arrays
            "   }" +
            "" +
            "   Entities" +
            "   {" +
            "       SpawnEntity(Skeleton1, [4, 5]);" +
            "   }" +
            "}";

        [SetUp]
        public void Setup()
        {
            ICharStream stream = CharStreams.fromString(TestCode);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            parseTree = new DazelParser(tokens) {BuildParseTree = true}.start();
        }

        [Test]
        public void TypeCheck_Visit_AllScopesAccountedFor()
        {
            var ast = new AstBuilder().BuildAst(parseTree);
            var tc = new TypeChecker(ast);
            
            
            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            // We know that, if the stack height is 0, then the scopes have all been popped off the stack.
            Assert.That(tc.EnvironmentStack.Count == 0);
        }
    }
}