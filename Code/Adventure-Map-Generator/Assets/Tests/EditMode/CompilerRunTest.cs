﻿using Dazel.Compiler;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;

namespace Tests.EditMode
{
    [TestFixture]
    public class CompilerRunTest
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
            "       s2exit1 = Exit([0, 0], SampleScreen1.Exits.s1exit1);" + 
            "   }" +
            "}";

        [Test]
        public void DazelCompiler_TryRun_SucceedOnValidSourceCode()
        {
            DazelCompiler dazelCompiler = new DazelCompiler(TestCode1_1, TestCode1_2);
            bool TestDelegate() => dazelCompiler.TryRun(out _);
            Assert.True(TestDelegate());
            Assert.DoesNotThrow(() => TestDelegate());
        }
        
        private const string TestCode2_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       s1exit1 = Exit([0, 0], SampleScreen2.Exits.s2exit1);" + 
            "   }" +
            "}";
        
        private const string TestCode2_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "   }" +
            "}";

        [Test]
        public void DazelCompiler_TryRun_ThrowOnValidSourceCode()
        {
            void TestDelegate()
            {
                UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
                DazelCompiler dazelCompiler = new DazelCompiler(TestCode2_1, TestCode2_2);
                DazelLogger.ThrowExceptions = true;
                
                Assert.False(dazelCompiler.TryRun(out _));
            }
            Assert.DoesNotThrow(TestDelegate);
        }
    }
}