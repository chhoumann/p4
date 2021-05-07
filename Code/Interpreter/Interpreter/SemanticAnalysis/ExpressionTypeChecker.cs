using System;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Visitors;

namespace Interpreter.SemanticAnalysis
{
    internal sealed class ExpressionTypeChecker : IExpressionVisitor
    {
        private readonly AbstractSyntaxTree ast;
        private readonly SymbolTable<SymbolTableEntry> symbolTable;

        public ExpressionTypeChecker(AbstractSyntaxTree ast, SymbolTable<SymbolTableEntry> symbolTable)
        {
            this.ast = ast;
            this.symbolTable = symbolTable;
        }
        
        private SymbolType currentType = SymbolType.Null;
        private SymbolType CurrentType
        {
            get => currentType;
            set
            {
                if (currentType == SymbolType.Null || currentType == SymbolType.Integer && (value == SymbolType.Float || value == SymbolType.Integer))
                {
                    currentType = value;
                    return;
                }
                
                throw new InvalidOperationException($"Type mismatch. {value} is not {currentType}");
            }
        }
        
        public SymbolType GetType(ExpressionNode expression)
        {
            expression.Accept(this);
            
            return CurrentType;
        }
        
        public void Visit(SumExpression sumExpression)
        {
            sumExpression.Left.Accept(this);
            sumExpression.Right.Accept(this);
        }
        
        public void Visit(FactorExpression factorExpression)
        {
            factorExpression.Left.Accept(this);
            factorExpression.Right.Accept(this);
        }

        public void Visit(TerminalExpression terminalExpression)
        {
            terminalExpression.Child.Accept(this);
        }
        
        public void Visit(MemberAccess memberAccess)
        {
            if (ast.TryRetrieveNode(memberAccess.Identifiers, out ValueNode value))
            {
                CurrentType = value.Type;
            }
            else
            {
                throw new InvalidOperationException($"{String.Join('.', memberAccess.Identifiers)} was not found.");
            }
        }

        public void Visit(FloatValue floatValue)
        {
            CurrentType = floatValue.Type;
        }

        public void Visit(IdentifierValue identifierValue)
        {
            CurrentType = symbolTable.RetrieveSymbol(identifierValue.Value).Type;
        }

        public void Visit(IntValue intValue)
        {
            CurrentType = intValue.Type;
        }

        public void Visit(ArrayNode arrayNode)
        {
            CurrentType = arrayNode.Type;

            SymbolType valueType = arrayNode.Values[0].Type;
            foreach (ValueNode value in arrayNode.Values)
            {
                if (value.Type == valueType)
                {
                    valueType = value.Type;
                }
                else
                {
                    throw new InvalidOperationException($"Type mismatch. {value} is not {currentType}");
                }
            }
        }

        public void Visit(StringNode stringNode)
        {
            CurrentType = stringNode.Type;
        }

        public void Visit(ExitValue exitValue)
        {
            
        }

        #region IVisitor unimplemented
        public void Visit(SumOperation sumOperation)
        {
        }
        
        public void Visit(FactorOperation factorOperation)
        {
        }
        #endregion
    }
}