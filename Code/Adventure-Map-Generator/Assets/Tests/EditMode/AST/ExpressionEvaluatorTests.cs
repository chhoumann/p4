﻿using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using NUnit.Framework;

namespace Tests.EditMode.AST
{
    [TestFixture]
    public class ExpressionEvaluatorTests : DazelTestBase
    {
        [Test]
        public void ExpressionEvaluator_Visit_SimpleAddition()
        {
            SumExpressionNode sumExpression = new SumExpressionNode
            {
                Left = new IntValueNode {Value = 1},
                OperationNode = new SumOperationNode {Operator = Operators.AddOp},
                Right = new IntValueNode {Value = 2}
            };
            
            ExpressionEvaluator<int> evaluator = new ExpressionEvaluator<int>(new IntCalculator(sumExpression.Token));
            sumExpression.Accept(evaluator);

            Assert.That(evaluator.Result == 3, "evaluator.Result == 3");
        }
        
        [Test]
        public void ExpressionEvaluator_Visit_FactorOperation()
        {
            FactorExpressionNode sumExpression = new FactorExpressionNode()
            {
                Left = new FloatValueNode {Value = 10.5f},
                OperationNode = new FactorOperationNode() {Operator = Operators.MultOp},
                Right = new IntValueNode {Value = 5}
            };
            
            ExpressionEvaluator<float> evaluator = new ExpressionEvaluator<float>(new FloatCalculator(sumExpression.Token));
            sumExpression.Accept(evaluator);

            Assert.That(evaluator.Result == 52.5f, "evaluator.Result == 52.5");
        }
    }
}