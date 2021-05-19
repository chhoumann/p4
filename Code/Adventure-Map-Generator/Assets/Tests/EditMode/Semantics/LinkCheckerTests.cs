using System;
using Dazel.Compiler.Ast;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;

namespace Tests.EditMode.Semantics
{
    public sealed class ExitCheckerTests
    {
        [TearDown]
        public void CleanUp() => EnvironmentStore.CleanUp();
        
        private const string TestCode1_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       s1exit1 = Exit([0, 0], SampleScreen2.Exits.s2exit1);" + 
            "   }" +
            "}";
        
        private const string TestCode1_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "   }" +
            "}";

        [Test]
        public void LinkCheck_Visit_ThrowOnInvalidExitLink()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1, TestCode1_2);
            Linker tc = new Linker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<Exception>(TestDelegate);
        }
        
        private const string TestCode2_1 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       s2exit1 = Exit([1, 1], SampleScreen2.Exits.s1exit1);" + 
            "   }" +
            "}";

        [Test]
        public void LinkCheck_Visit_SuccessOnValidLink()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1, TestCode2_1);
            Linker tc = new Linker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            Assert.DoesNotThrow(TestDelegate);
        }
        
        private const string TestCode3_1 = 
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       x = SampleScreen2.Exits.s2exit1;" + 
            "   }" +
            "}";
        
        private const string TestCode3_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       s2exit1 = Exit([1, 1], SampleScreen2.Exits.s1exit1);" + 
            "   }" +
            "}";

        [Test]
        public void LinkCheck_Visit_ValidMemberAccessSucceeds()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode3_1, TestCode3_2);
            Linker tc = new Linker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            Assert.DoesNotThrow(TestDelegate);
        }
        
        private const string TestCode3_3 = 
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       x = SampleScreen2.Exits.s2exit2;" + 
            "   }" +
            "}";
        
        [Test]
        public void LinkCheck_Visit_InValidMemberAccessThrows()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode3_3, TestCode3_2);
            Linker tc = new Linker(ast);
            
            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            Assert.Throws<Exception>(TestDelegate);
        }
    }
}