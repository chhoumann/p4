using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ExpressionEvaluator<T> : IExpressionVisitor
    {
        public T Result { get; private set; }
        private readonly Calculator<T> calculator;

        public ExpressionEvaluator(Calculator<T> calculator)
        {
            this.calculator = calculator;
        }
        
        public void Visit(SumExpressionNode sumExpressionNode)
        {
            sumExpressionNode.Left.Accept(this);
            T a = Result;
                    
            sumExpressionNode.Right.Accept(this);
            T b = Result;
            
            switch (sumExpressionNode.OperationNode.Operator)
            {
                case Operators.AddOp:
                    Result = calculator.Add(a, b);
                    break;
                case Operators.MinOp:
                    Result = calculator.Subtract(a, b);
                    break;
                default:
                    DazelLogger.EmitError($"Operation {sumExpressionNode.OperationNode.Operator} is not valid.", sumExpressionNode.Token);
                    break;
            }
        }

        public void Visit(SumOperationNode sumOperationNode)
        {
            throw new System.NotImplementedException();
        }
        
        public void Visit(FactorExpressionNode factorExpressionNode)
        {
            factorExpressionNode.Left.Accept(this);
            T a = Result;
                    
            factorExpressionNode.Right.Accept(this);
            T b = Result;
            
            switch (factorExpressionNode.OperationNode.Operator)
            {
                case Operators.MultOp:
                    Result = calculator.Multiply(a, b);
                    break;
                case Operators.DivOp:
                    Result = calculator.Divide(a, b);
                    break;
                default:
                    DazelLogger.EmitError($"Operation {factorExpressionNode.OperationNode.Operator} is not valid.", factorExpressionNode.Token);
                    break;
            }
        }

        public void Visit(FactorOperationNode factorOperationNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
            terminalExpressionNode.Child.Accept(this);
        }

        public void Visit(MemberAccessNode memberAccessNode)
        {
            ValueNode member = EnvironmentStore.AccessMember(memberAccessNode).ValueNode;
            
            switch (member)
            {
                case ArrayNode arrayNode:
                    Result = calculator.GetValue(arrayNode);
                    break;
                case ScreenExitValueNode screenExitValueNode:
                    screenExitValueNode.Accept(this);
                    break;
                case TileExitValueNode tileExitValueNode:
                    tileExitValueNode.Accept(this);
                    break;
                case IntValueNode intValueNode:
                    Result = calculator.GetValue(intValueNode.Value);
                    break;
                case StringNode stringNode:
                    Result = calculator.GetValue(stringNode.Value);
                    break;
                case FloatValueNode floatValueNode:
                    Result = calculator.GetValue(floatValueNode.Value);
                    break;
            }
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            Result = calculator.GetValue(floatValueNode.Value);
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            identifierValueNode.ValueNode.Accept(this);
        }

       public void Visit(IntValueNode intValueNode)
        {
            Result = calculator.GetValue(intValueNode.Value);
        }

        public void Visit(ArrayNode arrayNode)
        {
            Result = calculator.GetValue(arrayNode);
        }

        public void Visit(StringNode stringNode)
        {
            Result = calculator.GetValue(stringNode.Value);
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            Result = calculator.GetValue(exitValueNode);
        }
    }
}