using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
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
            "           SomeVar2 = 1 + 2 / 3;" + // Test expressions & integers
            "           x = SomeVar2;" + // Test identifier values
            "       }" +
            "       let = Player.Health;" + // Test member access 
            "       arr = [1, 2, 3];" + // Test arrays
            "   }" +
            "" +
            "   Entities" +
            "   {" +
            "       SpawnEntity(Skeleton1, [4, 5]);" +
            "   }" +
            "}";

        private AbstractSyntaxTree BuildAst(string code)
        {
            ICharStream stream = CharStreams.fromString(code);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            IParseTree parseTree = new DazelParser(tokens) {BuildParseTree = true}.start();
            return new AstBuilder().BuildAst(parseTree);
        }

        [Test]
        public void TypeCheck_Visit_AllScopesAccountedFor()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode);
            TypeChecker tc = new TypeChecker(ast);

            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            // We know that, if the stack height is 0, then the scopes have all been popped off the stack.
            Assert.That(tc.EnvironmentStack.Count == 0);
        }
        
        private const string TestCode2 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       arr = [1, 2, 3] + 3;" +
            "       Size(30, 24);" +
            "       SomeVar1 = 2.0;" +
            "       { " +
            "           SomeVar2 = 1 + 2 / 3;" +
            "           x = SomeVar2 + 2;" +
            "       }" +
            "       let = SampleScreen1.Exits.exit1;" + 
            "   }" +
            "" +
            "   Entities" +
            "   {" +
            "       SpawnEntity(Skeleton1, [4, 5]);" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_ArrayPlusIntegerFails()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode2);
            TypeChecker tc = new(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<InvalidOperationException>(TestDelegate);
        }
        
        private const string TestCode3 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       memAccExit = SampleScreen1.Map.Exit1;" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_MemberAccessNotFoundIfNotDeclared()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode3);
            TypeChecker tc = new(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<InvalidOperationException>(TestDelegate);
        }
    }
}