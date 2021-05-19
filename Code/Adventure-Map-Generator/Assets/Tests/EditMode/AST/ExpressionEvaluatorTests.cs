using System.Collections.Generic;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.AST
{
    [TestFixture]
    public class ExpressionEvaluatorTests
    {
        private const string TestCode1_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = 1 + 2;" +
            "       x = 5; " +
            "   }" +
            "}";
        
        [Test]
        public void ExpressionEvaluator_Visit_SimpleAddition()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1_1);

            var identifierList = new List<string> {"SampleScreen1", "Map", "x"};
            
            if (ast.TryRetrieveNode(identifierList, out string identifier, out ExpressionNode expressionNode))
            {
                ExpressionEvaluator<int> evaluator = new ExpressionEvaluator<int>(ast, new IntCalculator(expressionNode.Token));
                expressionNode.Accept(evaluator);

                Debug.Log(evaluator.Result);
                
                Assert.That(evaluator.Result == 3, "evaluator.Result == 3");
            }
            else
            {
                Assert.Fail("Identifier not found.");
            }
        }

        private const string TestCode2 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = 10.5 * 5;" +
            "   }" +
            "}";
        
        [Test]
        public void ExpressionEvaluator_Visit_FactorOperation()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode2);

            var identifierList = new List<string> {"SampleScreen1", "Map", "x"};
            
            if (ast.TryRetrieveNode(identifierList, out string identifier, out ExpressionNode expressionNode))
            {
                ExpressionEvaluator<float> evaluator = new ExpressionEvaluator<float>(ast, new FloatCalculator(expressionNode.Token));
                expressionNode.Accept(evaluator);
                
                Assert.That(evaluator.Result == 52.5f, "evaluator.Result == 52.5f");
            }
            else
            {
                Assert.Fail("Identifier not found.");
            }
        }
    }
}