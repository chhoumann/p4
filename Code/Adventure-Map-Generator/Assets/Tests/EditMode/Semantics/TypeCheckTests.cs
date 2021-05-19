using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Semantics
{
    [TestFixture]
    public class TypeCheckTests : DazelTestBase
    {
        private void TestDelegate(AbstractSyntaxTree ast)
        {
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                new TypeChecker(ast).Visit(gameObject);
            }
            
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                new Linker(ast).Visit(gameObject);
            }
        }

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
            (AstBuilder astBuilder, List<IParseTree> parseTrees) = TestAstBuilder.GetAstBuilderAndParseTrees(TestCode);
            AbstractSyntaxTree ast = astBuilder.BuildAst(parseTrees);
            TypeChecker tc = new TypeChecker(ast);
            
            tc.Visit(ast.Root.GameObjects["SampleScreen1"]);
            
            // Expect one top symbol table and that all scopes have been popped off the stack.
            Assert.That(EnvironmentStore.TopSymbolTablesCount == 1, "EnvironmentStore.TopSymbolTables.Count == 1");
            Assert.That(astBuilder.EnvironmentStack.Count == 0, "tc.EnvironmentStack.Count == 0");
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

            Assert.Throws<Exception>(() =>
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode2);

                TestDelegate(ast);
            });
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
            Assert.Throws<Exception>(() =>
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode3);

                TestDelegate(ast);
            });
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
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode4_1, TestCode4_2);
            
            Assert.DoesNotThrow(() => TestDelegate(ast));
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
            "       exit1 = Exit([0, 0], SampleScreen1.Exits.exit1);" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_ExitCannotBeUsedInExpressions()
        {
            Assert.Throws<Exception>(() =>
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode4_3, TestCode4_4);

                TestDelegate(ast);
            });
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
            Assert.Throws<Exception>(() =>
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode6);
                TestDelegate(ast);
            });
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

            Assert.DoesNotThrow(() => TestDelegate(ast));

            List<string> variablePath = new List<string>() {"SampleScreen1", "Map", "x"};

            ValueNode value = EnvironmentStore.AccessMember(variablePath).ValueNode;
            
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
            Assert.Throws<Exception>(() =>
            {
                AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode8);

                TestDelegate(ast);
            });
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
            Assert.DoesNotThrow(() => TestDelegate(ast));

            List<string> variablePath = new List<string>() {"SampleScreen1", "Map", "x"};
            
            ValueNode value = EnvironmentStore.AccessMember(variablePath).ValueNode;
            
            Assert.That(value != null, "value != null");
        }
    }
}