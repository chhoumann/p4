using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;
using UnityEngine;

namespace Dazel.Tests.EditMode.Semantics
{
    [TestFixture]
    public class TypeCheckTests
    {
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
            "       arr = [1, 2, 3];" + // Test arrays
            "   }" +
            "" +
            "   Entities" +
            "   {" +
            "       SpawnEntity(\"Skeleton1\", [4, 5]);" +
            "   }" +
            "}";

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
            "   }" +
            "" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_ArrayPlusIntegerFails()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode2);
            TypeChecker tc = new TypeChecker(ast);

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
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<InvalidOperationException>(TestDelegate);
        }
        
        private const string TestCode4_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       exit1 = Exit([0, 0], SampleScreen2.Exits.exit1);" + 
            "   }" +
            "}";

        private const string TestCode4_2 =
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       exit1 = Exit([0, 0], SampleScreen1.Exits.exit1)" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_MemberAccessValidExpressionSucceeds()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode4_1, TestCode4_2);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.DoesNotThrow(TestDelegate);
        }
        
        private const string TestCode5_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       exit1 = Exit([0, 0], SampleScreen2.Exits.exit1);" + 
            "   }" +
            "}";
        
        private const string TestCode5_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "   }" +
            "}";

        [Test]
        public void TypeCheck_Visit_MemberAccessInvalidExpressionFails()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode5_1, TestCode5_2);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<InvalidOperationException>(TestDelegate);
        }
        
        private const string TestCode6 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = 50 - Size(39, 29);" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_AddNullFunctionInvocationToInteger()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode6);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<InvalidOperationException>(TestDelegate);
        }
        
        private const string TestCode7 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = Size(39, 29);" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_AssignNullFunctionToVariable()
        {
            AbstractSyntaxTree ast = BuildAst(TestCode7);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            List<string> l = new List<string>() {"SampleScreen1", "Map", "x"};

            ast.TryRetrieveNode(l, out AssignmentNode node);

            Assert.That(node.Expression == null);
            Assert.That(node.Identifier == "x");
        }

        private AbstractSyntaxTree BuildAst(params string[] code)
        {
            List<IParseTree> parseTrees = new List<IParseTree>();

            foreach (string s in code)
            {
                ICharStream stream = CharStreams.fromString(s);
                ITokenSource lexer = new DazelLexer(stream);
                ITokenStream tokens = new CommonTokenStream(lexer);
                DazelParser parser = new DazelParser(tokens) {BuildParseTree = true};
                parser.AddErrorListener(new DazelErrorListener(new DazelErrorLogger()));

                parseTrees.Add(parser.start());
            }

            return new AstBuilder().BuildAst(parseTrees);
        }
    }
}