using System;
using System.Collections.Generic;
using System.Numerics;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;
using UnityEngine;

namespace Dazel.Tests.EditMode.Semantics
{
    public class ExitCheckerTests
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
        public void ExitCheck_Visit_ThrowOnInvalidExitLink()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1, TestCode1_2);
            ExitChecker tc = new ExitChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<InvalidOperationException>(TestDelegate);
        }
        
        private const string TestCode2_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       s2exit1 = Exit([1, 1], SampleScreen2.Exits.s1exit1);" + 
            "   }" +
            "}";

        [Test]
        public void ExitCheck_Visit_SuccessOnValidLink()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1, TestCode2_2);
            ExitChecker tc = new ExitChecker(ast);

            List<string> samplescreen2exit = new List<string>() {"SampleScreen2", "Exits", "s2exit1"};
            bool wasFound = ast.TryRetrieveNode(samplescreen2exit, out string identifier, out TileExitValueNode exitValueNode);
            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            Assert.DoesNotThrow(TestDelegate);
            Assert.True(wasFound);
            Debug.Log(exitValueNode);
        }
    }
}