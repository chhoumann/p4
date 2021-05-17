using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;
using UnityEngine;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ExpressionEvaluator<T, S> : IExpressionVisitor
        where S : Calculator<T>, new()
    {
        public T Result { get; private set; }
        private readonly AbstractSyntaxTree ast;
        private Calculator<T> calculator = new S();

        public ExpressionEvaluator(AbstractSyntaxTree ast)
        {
            this.ast = ast;
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
                    throw new InvalidOperationException($"Operation {sumExpressionNode.OperationNode.Operator} is not valid.");
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
                    throw new InvalidOperationException($"Operation {factorExpressionNode.OperationNode.Operator} is not valid.");
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
            List<string> identifierList = memberAccessNode.Identifiers;
            
            if (ast.TryRetrieveNode(identifierList, out string _, out ValueNode valueNode))
            {
                switch (valueNode)
                {
                    case IntValueNode intValueNode:
                        Result = calculator.GetValue(intValueNode.Value);
                        break;
                    case FloatValueNode floatValueNode:
                        Result = calculator.GetValue(floatValueNode.Value);
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"Identifier {string.Join(", ", identifierList)} with type {valueNode.Type} is not supported.");
                }
            }
            else
            {
                throw new InvalidOperationException($"Identifier {string.Join(", ", identifierList)} was used but could not be found.");
            }
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            Result = calculator.GetValue(floatValueNode.Value);
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            switch (identifierValueNode.ValueNode)
            {
                case IntValueNode intValueNode:
                    Result = calculator.GetValue(intValueNode.Value);
                    break;
                case FloatValueNode floatValueNode:
                    Result = calculator.GetValue(floatValueNode.Value);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"Identifier {string.Join(", ", identifierValueNode.Identifier)} with type {identifierValueNode.Type} is not supported.");
            }
        }

       public void Visit(IntValueNode intValueNode)
        {
            Result = calculator.GetValue(intValueNode.Value);
        }

        public void Visit(ArrayNode arrayNode)
        {
            throw new InvalidOperationException($"Array ({arrayNode}) cannot be used in expressions.");
        }

        public void Visit(StringNode stringNode)
        {
            throw new InvalidOperationException($"String ({stringNode.Value}) cannot be used in expressions.");
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            throw new InvalidOperationException($"ExitValueNode cannot be used in expressions.");
        }
    }
}