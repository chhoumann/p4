using System;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;

namespace Tests.EditMode.Semantics
{
    public sealed class ExitCheckerTests : DazelTestBase
    {
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
        public void Linker_Visit_ThrowOnInvalidExitLink()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1, TestCode1_2);
            Linker linker = new Linker(ast);

            void TestDelegate() => linker.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<Exception>(TestDelegate);
        }
        
        private const string TestCode2_1 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       s2exit1 = Exit([1, 1], SampleScreen1.Exits.s1exit1);" + 
            "   }" +
            "}";

        [Test]
        public void Linker_Visit_SuccessOnValidLink()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1, TestCode2_1);

            void TestDelegate()
            {
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new TypeChecker().Visit(gameObject);
                }
            
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new Linker(ast).Visit(gameObject);
                }
            }
            
            Assert.DoesNotThrow(TestDelegate);
        }
        
        private const string TestCode3_1 = 
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       s1exit1 = Exit([1, 1], SampleScreen2.Exits.s2exit1);" + 
            "   }" +
            "}";
        
        private const string TestCode3_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       s2exit1 = Exit([1, 1], SampleScreen1.Exits.s1exit1);" + 
            "   }" +
            "}";

        [Test]
        public void Linker_Visit_ValidMemberAccessSucceeds()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode3_1, TestCode3_2);
            
            void TestDelegate()
            {
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new TypeChecker().Visit(gameObject);
                }
            
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new Linker(ast).Visit(gameObject);
                }
            }
            
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
        public void Linker_Visit_InValidMemberAccessThrows()
        {
            Assert.Throws<Exception>(() =>
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode3_3, TestCode3_2);
                Linker linker = new Linker(ast);
                linker.Visit(ast.Root.GameObjects["SampleScreen1"]);
            });
        }
    }
}