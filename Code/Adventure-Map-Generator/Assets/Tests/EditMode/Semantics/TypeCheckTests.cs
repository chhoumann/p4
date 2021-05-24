using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;

namespace Tests.EditMode.Semantics
{
    [TestFixture]
    public class TypeCheckTests : DazelTestBase
    {
        private void TestDelegate(AbstractSyntaxTree ast)
        {
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                new TypeChecker().Visit(gameObject);
            }
            
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                new LinkChecker(ast).Visit(gameObject);
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
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode);
            TypeChecker tc = new TypeChecker();
            
            foreach (GameObjectNode gameObjectNode in ast.Root.GameObjects.Values)
            {
                tc.Visit(gameObjectNode);
            }
            
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
            "       y = 1.5 + 1;" +
            "   }" +
            "}";
        
        [Test]
        public void TypeCheck_Visit_IntegerPlusFloatSucceeds()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode9);
            Assert.DoesNotThrow(() => TestDelegate(ast));

            List<string> variablePathX = new List<string>() {"SampleScreen1", "Map", "x"};
            List<string> variablePathY = new List<string>() {"SampleScreen1", "Map", "y"};
            
            ValueNode valueX = EnvironmentStore.AccessMember(variablePathX).ValueNode;
            ValueNode valueY = EnvironmentStore.AccessMember(variablePathY).ValueNode;
            
            Assert.That(valueX != null, "valueX != null");
            Assert.That(valueX is FloatValueNode {Value: 2.5f}, "valueX != null");
            
            Assert.That(valueY != null, "valueY != null");
            Assert.That(valueY is FloatValueNode {Value: 2.5f}, "valueY != null");
        }
    }
}