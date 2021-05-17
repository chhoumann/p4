using System.Collections.Generic;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Tests.EditMode.AST
{
    [TestFixture]
    public class ExpressionEvaluatorTests
    {
        private const string TestCode1 =
            "Screen SampleScreen1" +
            "{" +
            "   Map" +
            "   {" +
            "       x = 1 + 2;" +
            "   }" +
            "}";
        
        [Test]
        public void ExpressionEvaluator_Visit_SimpleAddition()
        {
            AbstractSyntaxTree ast = TestAstBuilder.BuildAst(TestCode1);

            var identifierList = new List<string> {"SampleScreen1", "Map", "x"};
            
            if (ast.TryRetrieveNode(identifierList, out string identifier, out ExpressionNode expressionNode))
            {
                ExpressionEvaluator<int, IntCalculator> evaluator = new ExpressionEvaluator<int, IntCalculator>(ast);
                expressionNode.Accept(evaluator);
                
                Assert.That(evaluator.Result == 3, "evaluator.Result == 3");
                Debug.Log(evaluator.Result);
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
                ExpressionEvaluator<float, FloatCalculator> evaluator = new ExpressionEvaluator<float, FloatCalculator>(ast);
                expressionNode.Accept(evaluator);
                
                Assert.That(evaluator.Result == 52.5f, "evaluator.Result == 52.5");
                Debug.Log(evaluator.Result);
            }
            else
            {
                Assert.Fail("Identifier not found.");
            }
        }
    }
}