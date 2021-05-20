using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public class ExpressionEvaluator<T> : IExpressionVisitor
    {
        public T Result { get; protected set; }
        protected readonly Calculator<T> Calculator;

        public ExpressionEvaluator(Calculator<T> calculator)
        {
            Calculator = calculator;
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
                    Result = Calculator.Add(a, b);
                    break;
                case Operators.MinOp:
                    Result = Calculator.Subtract(a, b);
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
                    Result = Calculator.Multiply(a, b);
                    break;
                case Operators.DivOp:
                    Result = Calculator.Divide(a, b);
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

        public virtual void Visit(MemberAccessNode memberAccessNode)
        {
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            Result = Calculator.GetValue(floatValueNode.Value);
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            identifierValueNode.ValueNode.Accept(this);
        }

       public void Visit(IntValueNode intValueNode)
        {
            Result = Calculator.GetValue(intValueNode.Value);
        }

        public void Visit(ArrayNode arrayNode)
        {
            Result = Calculator.GetValue(arrayNode);
        }

        public void Visit(StringNode stringNode)
        {
            Result = Calculator.GetValue(stringNode.Value);
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            Result = Calculator.GetValue(exitValueNode);
        }
    }
}