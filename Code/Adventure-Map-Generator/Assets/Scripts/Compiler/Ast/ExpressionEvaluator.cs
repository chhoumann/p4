using System;
using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast
{
    public class ExpressionEvaluator : IExpressionVisitor
    {
        public float Result { get; private set; }
        private readonly AbstractSyntaxTree ast;

        public ExpressionEvaluator(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }
        
        public void Visit(SumExpressionNode sumExpressionNode)
        {
            sumExpressionNode.Left.Accept(this);
            float a = Result;
                    
            sumExpressionNode.Right.Accept(this);
            float b = Result;
            
            switch (sumExpressionNode.OperationNode.Operator)
            {
                case Operators.AddOp:
                    Result = a + b;
                    break;
                case Operators.MinOp:
                    Result = a - b;
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
            float a = Result;
                    
            factorExpressionNode.Right.Accept(this);
            float b = Result;
            
            switch (factorExpressionNode.OperationNode.Operator)
            {
                case Operators.DivOp:
                    Result = a / b;
                    break;
                case Operators.MultOp:
                    Result = a * b;
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
            VariableHandler(memberAccessNode.Identifiers);
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            Result = floatValueNode.Value;
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            List<string> identifierList = new List<string>() {identifierValueNode.Value};

            VariableHandler(identifierList);
        }

        private void VariableHandler(List<string> identifierList)
        {
            if (ast.TryRetrieveNode(identifierList, out string _, out ValueNode valueNode))
            {
                switch (valueNode)
                {
                    case IntValueNode intValueNode:
                        Result = intValueNode.Value;
                        break;
                    case FloatValueNode floatValueNode:
                        Result = floatValueNode.Value;
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

        public void Visit(IntValueNode intValueNode)
        {
            Result = intValueNode.Value;
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