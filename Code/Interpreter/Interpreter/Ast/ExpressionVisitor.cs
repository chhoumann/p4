using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Array = Interpreter.Ast.Nodes.ExpressionNodes.Array;

namespace Interpreter.Ast
{
    public sealed class ExpressionVisitor : IExpressionVisitor
    {
        
        public ExpressionNode VisitExpression(DazelParser.ExpressionContext context)
        {
            return VisitSumExpression(context.sumExpression());
        }

        public ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context)
        {
            if (context.ChildCount > 1)
            {
                return new SumExpression
                {
                    Left = VisitSumExpression(context.sumExpression()),
                    Operation = VisitSumOperation(context.sumOperation()),
                    Right = VisitFactorExpression(context.factorExpression())
                };
            }

            return VisitFactorExpression(context.factorExpression());
        }
        
        public ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context)
        {
            if (context.ChildCount > 1)
            {
                return new FactorExpression
                {
                    Left = VisitFactorExpression(context.factorExpression()),
                    Operation = VisitFactorOperation(context.factorOperation()),
                    Right = VisitTerminalExpression(context.terminalExpression())
                };
            }

            return VisitTerminalExpression(context.terminalExpression());
        }
        
        public ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context)
        {
            if (context.expression() != null)
            {
                return VisitExpression(context.expression());
            }

            return VisitValue(context.value());
        }
        
        public Value VisitValue(DazelParser.ValueContext context)
        {
            if (context.array() != null)
            {
                return VisitArray(context.array());
            }

            if (context.memberAccess() != null)
            {
                return VisitMemberAccess(context.memberAccess());
            }

            if (context.functionInvocation() != null)
            {
                return new StatementVisitor().VisitFunctionInvocation(context.functionInvocation()).Evaluate();
            }

            switch (context.terminalValue.Type)
            {
                case DazelLexer.IDENTIFIER:
                    return new IdentifierValue()
                    {
                        Value = context.GetText()
                    };
                case DazelLexer.INT:
                    return new IntValue()
                    {
                        Value = int.Parse(context.GetText())
                    };
                case DazelLexer.FLOAT:
                    return new FloatValue()
                    {
                        Value = float.Parse(context.GetText())
                    };
                default:
                    throw new ArgumentException("Invalid value passed.");
            }
        }
        
        public Array VisitArray(DazelParser.ArrayContext context)
        {
            return new()
            {
                Values = VisitValueList(context.valueList())
            };
        }

        public List<Value> VisitValueList(DazelParser.ValueListContext context)
        {
            List<Value> values = new();
            
            if (context.value() == null) return values;
            
            values.Add(VisitValue(context.value()));

            if (context.valueList() != null)
            {
                values.AddRange(VisitValueList(context.valueList()));
            }

            return values;
        }

        public FactorOperation VisitFactorOperation(DazelParser.FactorOperationContext context)
        {
            char op = context.DIVISION_OP() != null ? Operators.DivOp : Operators.MultOp;
            
            return new FactorOperation()
            {
                Operation = op
            };
        }
        
        public SumOperation VisitSumOperation(DazelParser.SumOperationContext context)
        {
            char op = context.PLUS_OP() != null ? Operators.AddOp : Operators.MinOp;

            return new SumOperation()
            {
                Operation = op
            };
        }
        
        public MemberAccess VisitMemberAccess(DazelParser.MemberAccessContext context)
        {
            MemberAccess memberAccess = new()
            {
                Identifiers =
                {
                    context.GetChild(0).GetText(),
                    context.GetChild(2).GetText(),
                    context.GetChild(4).GetText(),
                }
            };
            
            return memberAccess;
        }
    }
}