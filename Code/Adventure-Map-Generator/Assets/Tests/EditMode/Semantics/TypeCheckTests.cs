using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Semantics
{
    [TestFixture]
    public class TypeCheckTests
    {
        [TearDown]
        public void CleanUp() => EnvironmentStore.CleanUp();

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
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode);
            TypeChecker tc = new TypeChecker(ast);
            
            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            // Expect four scopes and one top symbol table
            Assert.That(EnvironmentStore.TopSymbolTablesCount == 1, "EnvironmentStore.TopSymbolTables.Count == 1");
            Assert.That(tc.EnvironmentStack.Count == 4, "tc.EnvironmentStack.Count == 4");
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
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode2);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<Exception>(TestDelegate);
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
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode3);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<Exception>(TestDelegate);
        }
        
        private const string TestCode4_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       a = 1 + 1;" + 
            "       exit1 = Exit([0, 0], SampleScreen2.Exits.exit1);" + 
            "   }" +
            "}";

        private const string TestCode4_2 =
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       exit1 = Exit([0, 0], SampleScreen1.Exits.exit1);" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_MemberAccessValidExpressionSucceeds()
        {
            void TestDelegate()
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode4_1, TestCode4_2);
                
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new TypeChecker(ast).Visit(gameObject);
                }
                
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new Linker(ast).Visit(gameObject);
                }
            }
            
            Assert.DoesNotThrow(TestDelegate);
        }
        
        private const string TestCode4_3 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       exit1 = Exit([0, 0], SampleScreen2.Exits.exit1);" + 
            "       exit1 = exit1 + exit1;" + 
            "   }" +
            "}";

        private const string TestCode4_4 =
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "       exit1 = Exit([0, 0], SampleScreen1.Exits.exit1)" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_ExitCannotBeUsedInExpressions()
        {
            void TestDelegate()
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode4_3, TestCode4_4);
                
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new TypeChecker(ast).Visit(gameObject);
                }
                
                foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
                {
                    new Linker(ast).Visit(gameObject);
                }
            }

            Assert.Throws<Exception>(TestDelegate);
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
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode6);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            Assert.Throws<Exception>(TestDelegate);
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
        public void TypeCheck_Visit_AssignNullFunctionToVariableAllowed()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode7);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            List<string> variablePath = new List<string>() {"SampleScreen1", "Map", "x"};

            ValueNode value = EnvironmentStore.AccessMember(variablePath).ValueNode;
            
            Assert.DoesNotThrow(TestDelegate);
            Assert.That(value == null);
        }
        
        private const string TestCode8 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = Size(39, 29) + 321;" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_NullFunctionExpressionPlusIntegerThrows()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode8);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            List<string> variablePath = new List<string>() {"SampleScreen1", "Map", "x"};
            
            ValueNode value = EnvironmentStore.AccessMember(variablePath).ValueNode;
            
            Assert.Throws<Exception>(TestDelegate);
            Assert.That(value == null);
        }
        
        private const string TestCode9 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = 1 + 1.5;" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_IntegerPlusFloatSucceeds()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode9);
            TypeChecker tc = new TypeChecker(ast);

            void TestDelegate() => tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            List<string> variablePath = new List<string>() {"SampleScreen1", "Map", "x"};
            
            ValueNode value = EnvironmentStore.AccessMember(variablePath).ValueNode;
            
            Assert.DoesNotThrow(TestDelegate);
            Assert.That(value != null, "value != null");
            Debug.Log(value);
        }
    }
}